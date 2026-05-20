# Session 02 — Plan & Notes

## Obiective sesiune

### 1. Ruta Promotions + componenta

- Adauga `src/components/Promotions/index.tsx`
- Inregistreaza ruta `/promotions` in `App.tsx`
- Componenta afiseaza lista de promotii venita de la API (`GET /api/promotions`)
- Functionalitati pe pagina: **filtre**, **sortare**, **paginare**

### 2. Fallback pentru rute necunoscute

Doua optiuni — de ales la implementare:

| Optiune | Comportament | Cum |
|---|---|---|
| Redirect la root | Orice ruta invalida -> `/` | `<Route path="*" element={<Navigate to="/" replace />} />` |
| Pagina 404 | Afiseaza un mesaj clar de eroare | `src/components/NotFound/index.tsx` + `<Route path="*">` |

Recomandat: **pagina 404** — mai prietenos cu utilizatorul, mai usor de extins.

### 3. Stilizarea aplicatiei

#### Paleta de culori (extrasa din logo)

| Token | Hex | Rol |
|---|---|---|
| Primary | `#C8C000` | Culoare principala (galben-lime din cos) |
| Primary Dark | `#6B6200` | Borduri, icons, outline (olive inchis) |
| Background | `#FFFFFF` / `#F9F8F2` | Fundal alb cald |
| Surface | `#F0EFE8` | Carduri, hover states |
| Text primary | `#2C2C1F` | Text principal (charcoal cald) |
| Text secondary | `#6B6A60` | Text secundar, placeholder |

#### Fisiere de creat / modificat

```
src/
  theme.ts          <- MUI ThemeProvider: paleta, tipografie, shape, overrides
  index.css         <- CSS reset + custom properties globale
  main.tsx          <- wrap App in <ThemeProvider theme={theme}>
```

#### Font

- **Inter** via `@fontsource/inter`  
- Install: `npm install @fontsource/inter`  
- Import in `main.tsx`: `import '@fontsource/inter'`

#### MUI Theme — structura de baza

```ts
import { createTheme } from '@mui/material/styles'

const theme = createTheme({
  palette: {
    primary:    { main: '#C8C000', dark: '#6B6200', contrastText: '#2C2C1F' },
    background: { default: '#F9F8F2', paper: '#FFFFFF' },
    text:       { primary: '#2C2C1F', secondary: '#6B6A60' },
  },
  typography: {
    fontFamily: '"Inter", sans-serif',
  },
  shape: {
    borderRadius: 10,
  },
})
```

---

## Backlog (sesiuni viitoare)

- [ ] Pagina Cart (`/cart`) — cos de cumparaturi cu badge pe Navbar
- [ ] ErrorBoundary — fallback pentru erori de runtime React
- [ ] Caching frontend cu **React Query** (`@tanstack/react-query`)
- [ ] Filtre + sortare pe pagina Products
- [ ] Pagina Promotions: paginare MUI `<Pagination>`

---

## Referinte utile

- [MUI Theming](https://mui.com/material-ui/customization/theming/)
- [MUI Palette](https://mui.com/material-ui/customization/palette/)
- [React Router v6 — No Match](https://reactrouter.com/en/main/start/concepts#not-found-matches)
- [React Query](https://tanstack.com/query/latest)
- [@fontsource/inter](https://fontsource.org/fonts/inter)
