import "./Header.css";
import { Link } from "react-router-dom";

function Header() {
  return (
    <>
      <header className="header header-fixed text-white sticky">
        <div className="container-fluid">
          <nav className="navbar navbar-expand-lg">
            <a className="navbar-brand" href="/">
              <img src="/assets/images/logo.svg"alt="logo"width="150"className="logo"/>
              <img src="/assets/images/logo-white.svg" alt="white logo"  width="150" className="logo-white" />
            </a>

            <button
              id="mobile-nav-toggler"
              className="hamburger hamburger--collapse"
              type="button"
            >
              <span className="hamburger-box">
                <span className="hamburger-inner"></span>
              </span>
            </button>

            <div className="navbar-collapse" id="main-nav">
              <ul className="navbar-nav mx-auto" id="main-menu">
                <li className="nav-item dropdown">
                  <button
                    type="button"
                    className="nav-link dropdown-toggle btn btn-link"
                    data-bs-toggle="dropdown"
                    aria-expanded="false"
                  >
                    <Link  to="/home">
                        Home 
                      </Link>
                  </button>

                  <ul className="dropdown-menu">
                    <li>
                      <Link className="dropdown-item" to="/home">
                        Home Version 1
                      </Link>
                    </li>
                  </ul>
                </li>

                <li className="nav-item dropdown">
                  <a
                    className="nav-link dropdown-toggle"
                    href="#"
                    data-toggle="dropdown"
                    aria-haspopup="true"
                    aria-expanded="false"
                  >
                    Pages
                  </a>

                  <ul className="dropdown-menu">
                    <li>
                      <a className="dropdown-item" href="/404">
                        404 Page
                      </a>
                    </li>
                    <li className="divider" role="separator"></li>
                    <li>
                      <a className="dropdown-item" href="/celebrities-list">
                        Celebrities List
                      </a>
                    </li>
                    <li>
                      <a className="dropdown-item" href="/celebrities-grid">
                        Celebrities Grid
                      </a>
                    </li>
                    <li>
                      <a className="dropdown-item" href="/celebrity-detail">
                        Celebrity Detail
                      </a>
                    </li>
                    </ul>
                </li>
                <li className="nav-item dropdown">
                  <a
                    className="nav-link dropdown-toggle"
                    href="#"
                    data-toggle="dropdown"
                    aria-haspopup="true"
                    aria-expanded="false"
                  >
                    Blog
                  </a>

                  <ul className="dropdown-menu">
                    <li>
                      <a className="dropdown-item" href="/blog-list">
                        Blog List
                      </a>
                    </li>
                    <li>
                      <a className="dropdown-item" href="/blog-list-fullwidth">
                        Blog List Fullwidth
                      </a>
                    </li>
                  </ul>
                </li>

                <li className="nav-item">
                  <Link className="nav-link" to="/chat">
                    Contact Us
                  </Link>
                </li>
              </ul>

              <ul className="navbar-nav extra-nav">
                <li className="nav-item">
                  <a className="nav-link toggle-search" href="#">
                    <i className="fa fa-search"></i>
                  </a>
                </li>

                <li className="nav-item notification-wrapper">
                  <a className="nav-link notification" href="#">
                    <i className="fa fa-globe"></i>
                    <span className="notification-count">2</span>
                  </a>
                </li>

                <li className="nav-item m-auto">
                  <a
                    href="#login-register-popup"
                    className="btn btn-main btn-effect login-btn popup-with-zoom-anim"
                  >
                    <i className="icon-user"></i> login
                  </a>
                </li>
              </ul>
            </div>
          </nav>
        </div>
      </header>
    </>
  );
}

export default Header;
