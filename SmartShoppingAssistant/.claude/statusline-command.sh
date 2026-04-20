#!/usr/bin/env bash
# Claude Code status line script
# Displays: model | project (git branch) | context used% | 5h quota used%

input=$(cat)

model=$(echo "$input" | jq -r '.model.display_name // "unknown"')
project_dir=$(echo "$input" | jq -r '.workspace.project_dir // .cwd // ""')
project=$(basename "$project_dir")

# Git branch (skip optional lock to avoid contention)
branch=$(git -C "$project_dir" --no-optional-locks branch --show-current 2>/dev/null)

used_pct=$(echo "$input" | jq -r '.context_window.used_percentage // empty')
ctx_total=$(echo "$input" | jq -r '.context_window.context_window_size // empty')

five_hour_pct=$(echo "$input" | jq -r '.rate_limits.five_hour.used_percentage // empty')
five_hour_resets=$(echo "$input" | jq -r '.rate_limits.five_hour.resets_at // empty')

# Build an 8-character ASCII progress bar from a percentage (0-100).
# Filled cells use the block character, empty cells use a light shade.
make_bar() {
  local pct="$1"
  local bar_width=8
  local filled=$(( pct * bar_width / 100 ))
  [ "$filled" -gt "$bar_width" ] && filled=$bar_width
  local empty=$(( bar_width - filled ))
  local bar=""
  local i
  for (( i=0; i<filled; i++ )); do bar="${bar}█"; done
  for (( i=0; i<empty;  i++ )); do bar="${bar}░"; done
  printf '%s' "$bar"
}

# Build parts
parts=()

# Model
parts+=("$(printf '\033[0;36m%s\033[0m' "$model")")

# Project + branch
if [ -n "$branch" ]; then
  parts+=("$(printf '\033[0;33m%s\033[0m \033[0;35m(%s)\033[0m' "$project" "$branch")")
else
  parts+=("$(printf '\033[0;33m%s\033[0m' "$project")")
fi

# Context window usage with progress bar
if [ -n "$used_pct" ] && [ -n "$ctx_total" ]; then
  pct_int=$(printf '%.0f' "$used_pct")
  bar=$(make_bar "$pct_int")
  parts+=("$(printf 'ctx: \033[0;32m[%s] %d%%\033[0m' "$bar" "$pct_int")")
elif [ -n "$used_pct" ]; then
  pct_int=$(printf '%.0f' "$used_pct")
  bar=$(make_bar "$pct_int")
  parts+=("$(printf 'ctx: \033[0;32m[%s] %d%%\033[0m' "$bar" "$pct_int")")
fi

# 5-hour rolling quota usage with color-coded progress bar
if [ -n "$five_hour_pct" ]; then
  pct_int=$(printf '%.0f' "$five_hour_pct")
  if [ "$pct_int" -ge 90 ]; then
    color='\033[0;31m'
  elif [ "$pct_int" -ge 70 ]; then
    color='\033[0;33m'
  else
    color='\033[0;32m'
  fi
  bar=$(make_bar "$pct_int")
  if [ -n "$five_hour_resets" ]; then
    resets_in=$(( five_hour_resets - $(date +%s) ))
    if [ "$resets_in" -gt 0 ]; then
      mins=$(( resets_in / 60 ))
      reset_str="$(printf ' resets %dm' "$mins")"
    else
      reset_str=""
    fi
  else
    reset_str=""
  fi
  parts+=("$(printf "5h: ${color}[%s] %s%%\033[0m%s" "$bar" "$pct_int" "$reset_str")")
fi

# Join with separator
sep="$(printf ' \033[0;90m|\033[0m ')"
out=""
for part in "${parts[@]}"; do
  if [ -z "$out" ]; then
    out="$part"
  else
    out="$out$sep$part"
  fi
done

printf '%b\n' "$out"
