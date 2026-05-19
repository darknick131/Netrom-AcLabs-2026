import { useState } from 'react'
import Navbar from './components/Navbar'
import { Box } from '@mui/material'
import { Routes, Route } from 'react-router-dom'
import Home from './components/Home'
import Categories from './components/Categories'
import Products from './components/Products'
import './App.css'


function App() {
  return (
    <>
      <Box className="app">
        <Navbar />
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/categories" element={<Categories />} />
          <Route path="/products" element={<Products />} />
        </Routes>
      </Box>
    </>
  );
}

export default App;
