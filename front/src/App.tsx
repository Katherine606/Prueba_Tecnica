import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import Login from "./components/Login";
import TablaSalas from "./components/TablaSalas";
import "bootstrap/dist/css/bootstrap.min.css";

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<Login />} />
        <Route path="/login" element={<Login />} />

        <Route path="/salas" element={<TablaSalas />} />
      </Routes>
    </Router>
  );
}

export default App;
