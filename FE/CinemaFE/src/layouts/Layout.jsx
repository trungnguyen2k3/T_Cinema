import Header from "../components/LayoutComponent/Header";
import Footer from "../components/LayoutComponent/Footer";
import { Outlet } from "react-router-dom";

function Layout() {
  return (
    <div className="app-layout">
      <Header />
      <main className="main-content">
        <Outlet />
      </main>
      <Footer />
    </div>
  );
}

export default Layout;
