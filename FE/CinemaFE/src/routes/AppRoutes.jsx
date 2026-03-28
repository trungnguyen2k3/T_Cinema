import { Routes, Route, Navigate } from "react-router-dom";
import Home from "../components/HomeComponent/HomeComponent";
import Login from "../components/LoginComponent/LoginComponent";
import ChatComponent from "../components/ChatComponent/ChatComponent";
import Layout from "../layouts/Layout";
function AppRoutes() {
  return (
    <Routes>
      <Route path="/" element={<Navigate to="/home" />} />
      <Route element={<Layout />}>
        <Route path="/home" element={<Home />} />
        <Route path="/chat" element={<ChatComponent />} />
      </Route>

      {/* Thêm các route khác ở đây */}
      <Route path="/login" element={<Login />} />
    </Routes>
  );
}

export default AppRoutes;
