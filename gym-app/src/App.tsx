import "./App.css";
import { useEffect } from "react";
import { Navigation } from "./components/Navigation";
import { Route, Routes } from "react-router-dom";
import { ProductsPage } from "./pages/ProductsPage";
import { AboutPage } from "./pages/AboutPage";
import { CoachesPage } from "./pages/CoachesPage";

function App() {
  useEffect(() => {
    // Изменяем заголовок html-страницы
    console.log("Hello useEffect");
  }, []);

  return (
    <>
      <>
        <Navigation />
        <Routes>
          <Route path="/" element={<ProductsPage />} />
          <Route path="/about" element={<AboutPage />} />
          <Route path="/coaches" element={<CoachesPage />} />
        </Routes>
      </>
    </>
  );
}

export default App;
