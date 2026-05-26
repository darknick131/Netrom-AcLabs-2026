# Sesiunea 2 – Rezumat modificari frontend

## Tema rezolvata

### 1. Ruta `/promotions` cu componenta aferenta

**Fisier nou:** `src/components/Promotions/index.tsx`

Componenta simpla care afiseaza un titlu si un mesaj placeholder pentru promotiile active.

**Modificari in `src/App.tsx`:**
- Import `Promotions`
- Ruta adaugata: `<Route path="/promotions" element={<Promotions />} />`

**Modificari in `src/components/Navbar/index.tsx`:**
- Link `Promotions` adaugat in array-ul `navLinks`

---

### 2. Redirect la 404 pentru rute necunoscute

**Fisier nou:** `src/components/NotFound/index.tsx`

Pagina 404 cu:
- Text mare **404**
- Mesaj "Pagina nu a fost gasita."
- Buton **Inapoi la Home** care foloseste `useNavigate` pentru a redirecta la `/`

**Modificare in `src/App.tsx`:**
- Ruta wildcard adaugata: `<Route path="*" element={<NotFound />} />`

---

### 3. Stilizarea aplicatiei

**Fisier nou:** `src/theme.ts`

Tema MUI personalizata cu paleta galben-inchis:

| Token | Valoare |
|---|---|
| `primary.main` | `#C8C000` (galben) |
| `primary.dark` | `#6B6200` |
| `background.default` | `#F9F8F2` (crem deschis) |
| `text.primary` | `#2C2C1F` (maro inchis) |
| `AppBar background` | `#2C2C1F` |

Tema este aplicata global in `src/main.tsx` prin `<ThemeProvider theme={theme}>`.

Alte ajustari globale:
- Font: `Inter`
- `borderRadius: 10` pentru toate componentele
- Butoanele nu au `textTransform: uppercase` (mai natural)
- Active NavLink evidentiata cu culoare primara + background subtil

---

## Structura finala `src/`

```
src/
├── App.tsx                        # Routes definite
├── main.tsx                       # ThemeProvider + BrowserRouter
├── theme.ts                       # Tema MUI custom
├── index.css / App.css            # Stiluri globale
├── assets/
│   └── logo.png
└── components/
    ├── Navbar/index.tsx           # Navigatie cu NavLink activ
    ├── Home/index.tsx
    ├── Categories/index.tsx
    ├── Products/index.tsx
    ├── Promotions/index.tsx       # NOU
    └── NotFound/index.tsx         # NOU
```
