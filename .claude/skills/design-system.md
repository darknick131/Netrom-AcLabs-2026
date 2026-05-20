---
version: alpha
name: SSA-Shopify-Inspired-Design
description: An inspired interpretation of Shopify's design language adapted for the Smart Shopping Assistant palette. Two parallel design tracks share typographic DNA and a single pill-button vocabulary but diverge in canvas polarity. The marketing/hero pages live on near-black warm canvases with giant thin-weight Inter display type and a single lime-accented CTA. The transactional pages (products, promotions, checkout) flip to a cream-warm canvas with the same pill button vocabulary and Inter for UI body. The two tracks share typographic DNA but diverge sharply in canvas polarity — and that choice is the brand.

colors:
  primary: "#C8C000"
  ink: "#2C2C1F"
  on-primary: "#2C2C1F"
  on-dark: "#F9F8F2"
  canvas-night: "#1C1C14"
  canvas-night-elevated: "#2C2C1F"
  canvas-light: "#ffffff"
  canvas-cream: "#F9F8F2"
  surface-elevated-dark: "#3A3A28"
  shade-30: "#D8D8C4"
  shade-40: "#AEAE98"
  shade-50: "#787868"
  shade-60: "#585848"
  shade-70: "#3F3F2E"
  hairline-light: "#E4E4D0"
  hairline-dark: "#3A3A28"
  lime-10: "#F0F7A0"
  olive-10: "#E8EDB5"
  link-warm-1: "#8A8A6A"
  link-warm-2: "#9A9A7A"
  link-warm-3: "#BCBCA0"
  link-lime: "#B8B85A"

typography:
  display-xxl:
    fontFamily: "Inter, system-ui, sans-serif"
    fontSize: 96px
    fontWeight: 300
    lineHeight: 1.0
    letterSpacing: 2.4px
  display-xl:
    fontFamily: "Inter, system-ui, sans-serif"
    fontSize: 70px
    fontWeight: 300
    lineHeight: 1.0
    letterSpacing: 0
  display-lg:
    fontFamily: "Inter, system-ui, sans-serif"
    fontSize: 55px
    fontWeight: 300
    lineHeight: 1.16
    letterSpacing: 0
  display-md:
    fontFamily: "Inter, system-ui, sans-serif"
    fontSize: 48px
    fontWeight: 300
    lineHeight: 1.14
    letterSpacing: 0
  heading-xl:
    fontFamily: "Inter, system-ui, sans-serif"
    fontSize: 28px
    fontWeight: 600
    lineHeight: 1.28
    letterSpacing: 0.42px
  heading-lg:
    fontFamily: "Inter, system-ui, sans-serif"
    fontSize: 24px
    fontWeight: 500
    lineHeight: 1.14
    letterSpacing: 0.36px
  heading-md:
    fontFamily: "Inter, system-ui, sans-serif"
    fontSize: 20px
    fontWeight: 600
    lineHeight: 1.4
    letterSpacing: 0.3px
  heading-sm:
    fontFamily: "Inter, system-ui, sans-serif"
    fontSize: 18px
    fontWeight: 600
    lineHeight: 1.25
    letterSpacing: 0.72px
  body-lg:
    fontFamily: "Inter, system-ui, sans-serif"
    fontSize: 18px
    fontWeight: 550
    lineHeight: 1.56
    letterSpacing: 0
  body-md:
    fontFamily: "Inter, system-ui, sans-serif"
    fontSize: 16px
    fontWeight: 420
    lineHeight: 1.5
    letterSpacing: 0
  body-strong:
    fontFamily: "Inter, system-ui, sans-serif"
    fontSize: 16px
    fontWeight: 550
    lineHeight: 1.5
    letterSpacing: 0
  caption:
    fontFamily: "Inter, system-ui, sans-serif"
    fontSize: 14px
    fontWeight: 500
    lineHeight: 1.49
    letterSpacing: 0.28px
  micro:
    fontFamily: "Inter, system-ui, sans-serif"
    fontSize: 13px
    fontWeight: 500
    lineHeight: 1.5
    letterSpacing: -0.13px
  eyebrow-cap:
    fontFamily: "Inter, system-ui, sans-serif"
    fontSize: 12px
    fontWeight: 400
    lineHeight: 1.2
    letterSpacing: 0.72px
  code:
    fontFamily: "ui-monospace, SFMono-Regular, Menlo, Monaco, Consolas, monospace"
    fontSize: 16px
    fontWeight: 400
    lineHeight: 1.5
    letterSpacing: 0

rounded:
  xs: 4px
  sm: 5px
  md: 8px
  lg: 12px
  xl: 20px
  pill: 9999px

spacing:
  xxs: 2px
  xs: 4px
  sm: 8px
  md: 12px
  lg: 16px
  xl: 24px
  xxl: 32px
  huge: 64px

components:
  button-primary-pill:
    backgroundColor: "{colors.primary}"
    textColor: "{colors.on-primary}"
    typography: "{typography.body-md}"
    rounded: "{rounded.pill}"
    padding: 12px 24px
  button-primary-pill-pressed:
    backgroundColor: "{colors.shade-70}"
    textColor: "{colors.on-dark}"
    typography: "{typography.body-md}"
    rounded: "{rounded.pill}"
    padding: 12px 24px
  button-outline-on-dark:
    backgroundColor: "transparent"
    border: "2px solid {colors.on-dark}"
    textColor: "{colors.on-dark}"
    typography: "{typography.body-md}"
    rounded: "{rounded.pill}"
    padding: 12px 26px
  button-outline-on-light:
    backgroundColor: "{colors.canvas-light}"
    border: "1px solid {colors.ink}"
    textColor: "{colors.ink}"
    typography: "{typography.body-md}"
    rounded: "{rounded.pill}"
    padding: 12px 24px
  button-lime-pill:
    backgroundColor: "{colors.lime-10}"
    textColor: "{colors.ink}"
    typography: "{typography.body-md}"
    rounded: "{rounded.pill}"
    padding: 12px 24px
  text-input:
    backgroundColor: "{colors.canvas-light}"
    textColor: "{colors.ink}"
    typography: "{typography.body-md}"
    rounded: "{rounded.md}"
    padding: 10px 12px
    border: "1px solid {colors.hairline-light}"
  card-product:
    backgroundColor: "{colors.canvas-light}"
    textColor: "{colors.ink}"
    typography: "{typography.body-md}"
    rounded: "{rounded.lg}"
    padding: 32px
    shadow: "0 2px 2px rgba(44,44,31,0.04), 0 4px 4px rgba(44,44,31,0.04), 0 8px 8px rgba(44,44,31,0.04), 0 0 0 1px rgba(44,44,31,0.06)"
  card-promotion-featured:
    backgroundColor: "{colors.lime-10}"
    textColor: "{colors.ink}"
    typography: "{typography.body-md}"
    rounded: "{rounded.lg}"
    padding: 32px
  card-feature-cinematic:
    backgroundColor: "{colors.canvas-night-elevated}"
    textColor: "{colors.on-dark}"
    typography: "{typography.body-lg}"
    rounded: "{rounded.lg}"
    padding: 32px
  card-olive-band:
    backgroundColor: "{colors.olive-10}"
    textColor: "{colors.ink}"
    typography: "{typography.body-md}"
    rounded: "{rounded.lg}"
    padding: 32px
  pill-tag-lime:
    backgroundColor: "{colors.lime-10}"
    textColor: "{colors.ink}"
    typography: "{typography.eyebrow-cap}"
    rounded: "{rounded.pill}"
    padding: 4px 12px
  pill-tag-shade:
    backgroundColor: "{colors.shade-30}"
    textColor: "{colors.ink}"
    typography: "{typography.eyebrow-cap}"
    rounded: "{rounded.pill}"
    padding: 4px 12px
  nav-bar-dark:
    backgroundColor: "{colors.canvas-night}"
    textColor: "{colors.on-dark}"
    typography: "{typography.body-md}"
    border-bottom: "1px solid {colors.hairline-dark}"
    padding: 16px 24px
  nav-bar-light:
    backgroundColor: "{colors.canvas-cream}"
    textColor: "{colors.ink}"
    typography: "{typography.body-md}"
    border-bottom: "1px solid {colors.hairline-light}"
    padding: 16px 24px
  footer-dark:
    backgroundColor: "{colors.canvas-night}"
    textColor: "{colors.on-dark}"
    typography: "{typography.caption}"
    rounded: "{rounded.xs}"
    padding: 64px 24px
---

## Overview

The SSA design runs two parallel tracks that share Inter as the sole typeface and a single pill-button vocabulary, but diverge in canvas polarity. The **cinematic track** lives on `{colors.canvas-night}` (`#1C1C14`) — a warm near-black derived from the logo's olive undertones — with thin-weight (300) Inter display type at large sizes and a single lime-accented CTA. The **transactional track** flips to `{colors.canvas-cream}` (`#F9F8F2`), the warm cream that mirrors the logo's light field, with the same pill button system in inverse polarity (filled lime or dark pill).

The lime accent (`{colors.primary}` `#C8C000`) is derived directly from the logo. On the cinematic track it shows as an eyebrow accent only; on the light track it appears as the featured-card fill and the primary CTA. The dark olive (`{colors.ink}` `#2C2C1F`) is the primary text and icon color on all light surfaces, and the base for the dark canvas.

**Key Characteristics:**
- Two-canvas system: `{colors.canvas-night}` for cinematic marketing/hero, `{colors.canvas-cream}` / `{colors.canvas-light}` for transactional surfaces.
- Pill shape (`{rounded.pill}`) is the only button shape across both tracks.
- Thin-weight (300) Inter for all display and hero type — the brand's editorial signature.
- Lime (`{colors.lime-10}` `#F0F7A0`) and olive (`{colors.olive-10}` `#E8EDB5`) are reserved for the light track only.
- Photography / hero visuals are full-bleed on the cinematic track, inset in `{rounded.lg}` cards on the light track.

---

## Colors

### Brand & Accent

- **Primary / Lime** (`{colors.primary}` — `#C8C000`): The logo color. Used as the featured-tier fill, primary CTA pill on dark surfaces, and eyebrow text on cinematic pages.
- **Lime-10** (`{colors.lime-10}` — `#F0F7A0`): Very light lime tint. Featured card background, pill tag fill on light surfaces. Signals commerce and action.
- **Olive-10** (`{colors.olive-10}` — `#E8EDB5`): Softer olive tone. Wide section bands on the light track to differentiate content categories without leaving the lime family.

### Surface

- **Canvas Night** (`{colors.canvas-night}` — `#1C1C14`): Warm deep black for hero, cinematic sections, footer.
- **Canvas Night Elevated** (`{colors.canvas-night-elevated}` — `#2C2C1F`): Cards and elevated surfaces on dark pages.
- **Surface Elevated Dark** (`{colors.surface-elevated-dark}` — `#3A3A28`): Olive-tinted dark surface for subtle depth on dark cards.
- **Canvas Light** (`{colors.canvas-light}` — `#ffffff`): Product cards, form inputs, pricing tables.
- **Canvas Cream** (`{colors.canvas-cream}` — `#F9F8F2`): Warm off-white page background on transactional surfaces. Matches the logo's warm light field.
- **Hairline Light** (`{colors.hairline-light}` — `#E4E4D0`): Warm-tinted 1px borders on light cards.
- **Hairline Dark** (`{colors.hairline-dark}` — `#3A3A28`): 1px borders on dark cards.

### Shade Ladder (warm olive-neutral)

- **Shade-30** (`{colors.shade-30}` — `#D8D8C4`): Tag background on light, hairline on dark.
- **Shade-40** (`{colors.shade-40}` — `#AEAE98`): Tertiary text on light, secondary on dark.
- **Shade-50** (`{colors.shade-50}` — `#787868`): Secondary text on light.
- **Shade-60** (`{colors.shade-60}` — `#585848`): Tertiary text on light.
- **Shade-70** (`{colors.shade-70}` — `#3F3F2E`): Pressed state of the primary pill button.

### Text

- **Ink** (`{colors.ink}` — `#2C2C1F`): All text on light canvas. Warm near-black.
- **On Dark** (`{colors.on-dark}` — `#F9F8F2`): All text on dark canvas. Warm cream, not pure white.
- **On Primary** (`{colors.on-primary}` — `#2C2C1F`): Text on lime `#C8C000` buttons. Dark ink because lime is a light color.

### Link Tones (dark surfaces)

- `{colors.link-warm-1}` `#8A8A6A` · `{colors.link-warm-2}` `#9A9A7A` · `{colors.link-warm-3}` `#BCBCA0` · `{colors.link-lime}` `#B8B85A`: Muted warm footer/tertiary links on dark surfaces.

---

## Typography

### Font Family

**Inter** (loaded via Google Fonts — already in `index.html`) is the sole typeface across all roles. Display sizes use weight 300 (thin). Body uses weight 420 (regular-light variable). Strong/button uses 550 (medium variable). The weight spread from 300 → 550 is the typographic range — nothing above 700 in normal layouts.

Inter Variable supports sub-weight precision via `font-weight` on browsers that load the variable font file. The `ss03` OpenType stylistic set can optionally be enabled globally for a slightly more geometric glyph set.

### Hierarchy

| Token | Size | Weight | Line Height | Letter Spacing | Use |
|---|---|---|---|---|---|
| `display-xxl` | 96px | 300 | 1.0 | 2.4px | Cinematic hero headline |
| `display-xl` | 70px | 300 | 1.0 | 0 | Section opener on cinematic pages |
| `display-lg` | 55px | 300 | 1.16 | 0 | Page title on light track |
| `display-md` | 48px | 300 | 1.14 | 0 | Sub-section headline, pricing page |
| `heading-xl` | 28px | 600 | 1.28 | 0.42px | Card title, pricing tier name |
| `heading-lg` | 24px | 500 | 1.14 | 0.36px | Compact card title |
| `heading-md` | 20px | 600 | 1.4 | 0.3px | Section sub-heading |
| `heading-sm` | 18px | 600 | 1.25 | 0.72px | Eyebrow / mini-section label |
| `body-lg` | 18px | 550 | 1.56 | 0 | Marketing body lead |
| `body-md` | 16px | 420 | 1.5 | 0 | Default UI body, button labels |
| `body-strong` | 16px | 550 | 1.5 | 0 | Emphasized body run |
| `caption` | 14px | 500 | 1.49 | 0.28px | Helper copy, footnotes |
| `micro` | 13px | 500 | 1.5 | -0.13px | Fine print |
| `eyebrow-cap` | 12px | 400 | 1.2 | 0.72px | All-caps eyebrow label |
| `code` | 16px | 400 | 1.5 | 0 | Code blocks |

### Principles

- **Thin weight is the display signature.** Always render display sizes at weight 300. At 400+ the brand loses its editorial feel.
- **Inter for everything.** No secondary typeface.
- **Positive tracking lifts thin glyphs.** The 96px hero gets +2.4px — optical air for the thin cut. At 70px and below, tracking returns to 0.
- **Eyebrow caps in `{colors.primary}` on dark, in `{colors.shade-50}` on light.** Eyebrows locate the brand accent without overloading.

---

## Layout

### Spacing System

Base unit: 8px.

| Token | Value | Use |
|---|---|---|
| `xxs` | 2px | Fine inner spacing |
| `xs` | 4px | Icon/tag inner padding |
| `sm` | 8px | Compact item gap |
| `md` | 12px | Button vertical padding |
| `lg` | 16px | Default gap, nav padding |
| `xl` | 24px | Card inner padding (compact) |
| `xxl` | 32px | Card inner padding (standard) |
| `huge` | 64px | Section vertical padding |

### Section Padding

- Cinematic hero: 96–128px vertical — extreme negative space is the brand.
- Transactional: 48–64px between bands.

### Grid

- Hero: full-width, max ~1440px, photography edge-bleeds the container.
- Product / promotions grid: 3-up desktop → 2-up tablet → 1-up mobile.
- Reading column (long-form): ~720–840px centered.

---

## Elevation & Depth

| Level | Shadow | Use |
|---|---|---|
| 0 | none | Default flat surface |
| 1 | `0 1px 0 rgba(248,247,242,0.04) inset` | Subtle top-edge sheen on dark cards |
| 2 | `0 0 0 1px rgba(248,247,242,0.08), 0 1px 3px rgba(0,0,0,0.3)` | Elevated dark card with hairline |
| 3 | `0 2px 2px rgba(44,44,31,0.04), 0 4px 4px rgba(44,44,31,0.04), 0 8px 8px rgba(44,44,31,0.04), 0 0 0 1px rgba(44,44,31,0.06)` | Stacked-shadow paper halo on light cards |
| 4 | `0 25px 50px -12px rgba(44,44,31,0.2)` | Modal / floating panel on light |

The **stacked tiny-shadow stack (Level 3)** is the brand's distinctive depth on the light track — four tiny shadows layered produce a soft paper-like halo. Dark cards use the inset top-edge highlight only.

---

## Shapes

| Token | Value | Use |
|---|---|---|
| `xs` | 4px | Input fields, hairline tags |
| `sm` | 5px | Small image containers |
| `md` | 8px | Form inputs, compact cards |
| `lg` | 12px | Product cards, feature cards, pricing |
| `xl` | 20px | Hero photo frames |
| `pill` | 9999px | All buttons, pill tags, category chips |

**Pill is the only button shape.** No rounded rectangles for buttons anywhere.

---

## Components

### Buttons

**`button-primary-pill`** — default CTA on both tracks.
Background `{colors.primary}` (`#C8C000`), text `{colors.on-primary}` (dark ink — lime is light), `{typography.body-md}`, padding 12px 24px, `{rounded.pill}`.

**`button-outline-on-dark`** — cinematic hero CTA.
Transparent background, `2px solid {colors.on-dark}`, text `{colors.on-dark}`, same pill geometry. On hover, border and text shift to `{colors.primary}`.

**`button-outline-on-light`** — light-track secondary.
Background `{colors.canvas-light}`, `1px solid {colors.ink}`, text `{colors.ink}`, same pill geometry.

**`button-lime-pill`** — featured / primary CTA on transactional pages.
Background `{colors.lime-10}`, text `{colors.ink}`, same pill geometry.

### Cards

**`card-product`** — product tile on the catalog page.
Background `{colors.canvas-light}`, `{rounded.lg}` 12px, padding 32px, Level-3 stacked shadow. Holds: lime pill tag (category), product name in `heading-xl`, price in `display-md` weight 300, CTA `button-primary-pill` pinned bottom.

**`card-promotion-featured`** — highlighted promotion tile.
Background `{colors.lime-10}`, otherwise identical to `card-product`. The lime fill (not a border) is the brand's featured signal — same language as Shopify's aloe tier.

**`card-feature-cinematic`** — feature card on dark hero sections.
Background `{colors.canvas-night-elevated}`, text `{colors.on-dark}`, Level-1 inset highlight, `{rounded.lg}`.

**`card-olive-band`** — wide horizontal category band on light track.
Background `{colors.olive-10}`, text `{colors.ink}`, `{rounded.lg}`, 32px padding.

### Pills & Tags

**`pill-tag-lime`** — category / feature label on light surfaces.
Background `{colors.lime-10}`, text `{colors.ink}`, `{typography.eyebrow-cap}` (12px uppercase, 0.72px tracking), `{rounded.pill}`, padding 4px 12px.

**`pill-tag-shade`** — neutral tag.
Background `{colors.shade-30}`, otherwise same shape as `pill-tag-lime`.

### Navigation

**`nav-bar-dark`** — default navbar.
Background `{colors.canvas-night}`, text `{colors.on-dark}`, bottom border `1px solid {colors.hairline-dark}`, no box-shadow. Logo left, nav links center, optional pill CTA right.

Active nav link: `{colors.primary}` text, subtle `rgba(200,192,0,0.12)` background fill.

### Inputs

**`text-input`** — form field.
Background `{colors.canvas-light}`, text `{colors.ink}`, `{typography.body-md}`, padding 10px 12px, `{rounded.md}` 8px, `1px solid {colors.hairline-light}` border.

### Footer

**`footer-dark`** — page footer on cinematic track.
Background `{colors.canvas-night}`, text `{colors.on-dark}`, `{typography.caption}`, padding 64px 24px. Link groups use muted warm tones (`{colors.link-warm-1}` etc.).

---

## Do's and Don'ts

### Do
- Reserve `{colors.lime-10}` and `{colors.olive-10}` for the light track only — never on dark canvases.
- Always use `{rounded.pill}` for buttons — no exceptions.
- Render display type at weight 300; bumping to 400+ kills the editorial feel.
- Use `{colors.on-dark}` (`#F9F8F2` warm cream) on dark surfaces, not pure white `#fff`.
- Use `{colors.on-primary}` (`#2C2C1F` dark ink) on lime buttons — lime is a light color, dark text is required for contrast.
- Apply the Level-3 stacked shadow on light-track product cards for soft depth.
- Pair dark canvas with cream text and outline pills; pair light canvas with ink text and filled-lime or filled-dark pills.

### Don't
- Don't put lime or olive fills on the cinematic dark track.
- Don't add box-shadows on dark-track cards beyond the inset top-edge highlight.
- Don't use white (`#ffffff`) text on lime (`#C8C000`) — contrast ratio fails WCAG AA.
- Don't use rounded-rectangle buttons — pill is the only button shape.
- Don't mix canvas polarities within a single page section.
- Don't use display type (weight 300) below 36px — at small sizes thin weight becomes illegible.

---

## Responsive Behavior

| Breakpoint | Width | Key Changes |
|---|---|---|
| Wide | ≥ 1440px | Full hero, 3-up product grid |
| Desktop | 1024–1440px | Default layout |
| Tablet | 768–1023px | 2-up product grid, hero crops |
| Mobile | < 768px | 1-up, hamburger nav, display-xxl → ~56px |

- Display scales: 96 → 70 → 55 → 48 → 36px through breakpoints.
- Product grid: 3-up → 2-up → 1-up.
- Pill buttons stay ≥ 44px touch targets on mobile via 12px vertical padding.

---

## Iteration Guide

1. Choose ONE canvas track per page section — cinematic or transactional, never blended.
2. Reference component names and tokens directly (`{colors.lime-10}`, `{button-primary-pill}`, `{rounded.pill}`).
3. Pill shape is non-negotiable for buttons; new variants differ in fill/border/canvas only.
4. Add new card variants as separate named entries.
5. Default body to `body-md`; reserve `body-lg` for marketing leads.
6. When designing a new page: pick canvas polarity first, then apply the matching component set.
