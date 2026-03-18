import React from "react";
import "./HomeComponent.css";
function Home() {
  return (
    <>
      <div className="hero-section">
        <div className="hero-overlay"></div>

        <div className="container hero-content">
          <div className="row justify-content-center text-center">
            <div className="col-lg-10">
              <h1 className="hero-title">START STREAMING NOW</h1>

              <p className="hero-subtitle">
                Watch the latest movies, series and cinema content in one place.
              </p>
            </div>
          </div>

          <div className="row justify-content-center">
            <div className="col-lg-8 col-md-10 col-12">
              <form className="hero-search-form">
                <div className="hero-search-box">
                  <input
                    type="text"
                    className="hero-search-input"
                    placeholder="Enter Movies or Series Title"
                  />

                  <button
                    type="submit"
                    className="hero-search-btn btn btn-main btn-effect"
                  >
                    <i className="fa fa-search"></i>
                  </button>
                </div>
              </form>
            </div>
          </div>
        </div>
      </div>
    </>
  );
}

export default Home;
