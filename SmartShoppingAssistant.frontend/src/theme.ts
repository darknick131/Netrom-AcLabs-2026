import { createTheme } from '@mui/material/styles'

const theme = createTheme({
  palette: {
    primary: {
      main: '#C8C000',
      dark: '#6B6200',
      contrastText: '#2C2C1F',
    },
    secondary: {
      main: '#6B6200',
      contrastText: '#FFFFFF',
    },
    background: {
      default: '#F9F8F2',
      paper: '#FFFFFF',
    },
    text: {
      primary: '#2C2C1F',
      secondary: '#6B6A60',
    },
  },
  typography: {
    fontFamily: '"Inter", system-ui, sans-serif',
    h1: { fontWeight: 700 },
    h2: { fontWeight: 700 },
    h3: { fontWeight: 600 },
    h4: { fontWeight: 600 },
    h5: { fontWeight: 500 },
    h6: { fontWeight: 500 },
  },
  shape: {
    borderRadius: 10,
  },
  components: {
    MuiButton: {
      styleOverrides: {
        root: {
          textTransform: 'none',
          fontWeight: 600,
        },
      },
    },
    MuiAppBar: {
      styleOverrides: {
        root: {
          backgroundColor: '#2C2C1F',
          color: '#F9F8F2',
          boxShadow: '0 2px 8px rgba(0,0,0,0.15)',
        },
      },
    },
    MuiCard: {
      styleOverrides: {
        root: {
          boxShadow: '0 2px 12px rgba(44,44,31,0.08)',
        },
      },
    },
  },
})

export default theme
