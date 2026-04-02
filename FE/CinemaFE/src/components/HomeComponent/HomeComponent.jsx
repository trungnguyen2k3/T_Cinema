import 'swiper/css';
import 'swiper/css/navigation';
import 'swiper/css/pagination';
import React from "react";
import "./HomeComponent.css";
// Import Swiper React components và styles
import { Swiper, SwiperSlide } from 'swiper/react';
import { Navigation, Pagination, Autoplay } from 'swiper/modules';
function Home() {
  // Cấu hình chung cho các Slider
  const swiperOptions = {
    modules: [Navigation, Pagination, Autoplay],
    spaceBetween: 20,
    slidesPerView: 4, // Mặc định hiện 4 phim
    navigation: true, // Hiện nút mũi tên trái phải
    pagination: { clickable: true }, // Hiện dấu chấm ở dưới
    autoplay: { delay: 3000, disableOnInteraction: false },
    breakpoints: {
      0: { slidesPerView: 1 },    // Mobile hiện 1
      576: { slidesPerView: 2 },  // Tablet nhỏ hiện 2
      992: { slidesPerView: 3 },  // Tablet lớn hiện 3
      1200: { slidesPerView: 4 }  // Desktop hiện 4
    }
  };

  return (
    <>
      {/* 1. HERO SECTION */}
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
                  <button type="submit" className="hero-search-btn">
                    <i className="fa fa-search"></i>
                  </button>
                </div>
              </form>
            </div>
          </div>
        </div>
      </div>

      {/* 2. LATEST MOVIES (Sử dụng Swiper) */}
      <section className="latest-movies ptb100">
        <div className="container">
          <div className="row">
            <div className="col-md-8"><h2 className="title">Latest Movies</h2></div>
            <div className="col-md-4 align-self-center text-right">
              <a href="#" className="btn btn-icon btn-main btn-effect">
                view all <i className="ti-angle-double-right"></i>
              </a>
            </div>
          </div>
          
          <div className="mt20">
            <Swiper {...swiperOptions}>
              <SwiperSlide>
                <div className="movie-box-1">
                  <div className="poster"><img src="assets/images/posters/poster-1.jpg" alt="" /></div>
                  <div className="buttons"><a href="#" className="play-video"><i className="fa fa-play"></i></a></div>
                  <div className="movie-details">
                    <h4 className="movie-title"><a href="#">Star Wars</a></h4>
                    <span className="released">14 Dec 2017</span>
                  </div>
                  <div className="stars">
                    <div className="rating"><i className="fa fa-star"></i><i className="fa fa-star"></i><i className="fa fa-star"></i><i className="fa fa-star"></i><i className="fa fa-star-o"></i></div>
                    <span>7.5 / 10</span>
                  </div>
                </div>
              </SwiperSlide>
              {/* Thêm các SwiperSlide tương tự cho các phim khác ở đây */}
              <SwiperSlide>
                 <div className="movie-box-1">
                    <div className="poster"><img src="assets/images/posters/poster-2.jpg" alt="" /></div>
                    <div className="movie-details">
                        <h4 className="movie-title"><a href="#">The Brain</a></h4>
                        <span className="released">20 Dec 2017</span>
                    </div>
                 </div>
              </SwiperSlide>
            </Swiper>
          </div>
        </div>
      </section>

      {/* 3. UPCOMING MOVIES */}
      <section className="upcoming-movies parallax ptb100" style={{backgroundImage: 'url(assets/images/posters/movie-collection.jpg)', backgroundSize: 'cover'}}>
        <div className="container">
          <div className="row justify-content-center text-center">
            <div className="col-md-7"><h2 className="title text-white">Upcoming Movies & TV Shows</h2></div>
          </div>
          <div className="row mt50">
            <div className="col-md-8">
              <div className="movie-box-1 upcoming-featured-item">
                <div className="poster"><img src="assets/images/movies/upcoming-featured-item.jpg" alt="" /></div>
                <div className="movie-details">
                  <h4 className="movie-title"><a href="#">Tomb Raider</a></h4>
                  <span className="released">Release Date: 15 Mar 2018</span>
                </div>
              </div>
            </div>
            <div className="col-md-4">
              <div className="movie-box-1 upcoming-item">
                <div className="poster"><img src="assets/images/movies/upcoming-item-1.jpg" alt="" /></div>
                <div className="movie-details"><h4 className="movie-title"><a href="#">The Jungle</a></h4></div>
              </div>
              <div className="movie-box-1 upcoming-item mt20">
                <div className="poster"><img src="assets/images/movies/upcoming-item-2.jpg" alt="" /></div>
                <div className="movie-details"><h4 className="movie-title"><a href="#">Fast and Furious</a></h4></div>
              </div>
            </div>
          </div>
        </div>
      </section>

      {/* 4. LATEST TV SHOWS (Sử dụng Swiper) */}
      <section className="latest-tvshows ptb100">
        <div className="container">
          <div className="row">
            <div className="col-md-8 col-sm-12"><h2 className="title">Latest TV Shows</h2></div>
            <div className="col-md-4 col-sm-12 align-self-center text-right">
              <a href="#" className="btn btn-icon btn-main btn-effect">
                view all <i className="ti-angle-double-right"></i>
              </a>
            </div>
          </div>

          <div className="mt20">
            <Swiper {...swiperOptions}>
              {/* Item 1 */}
              <SwiperSlide>
                <div className="movie-box-1">
                  <div className="poster"><img src="assets/images/posters/poster-5.jpg" alt="" /></div>
                  <div className="buttons"><a href="#" className="play-video"><i className="fa fa-play"></i></a></div>
                  <div className="movie-details">
                    <h4 className="movie-title"><a href="#">Daredevil</a></h4>
                    <span className="released">19 Apr 2015</span>
                  </div>
                </div>
              </SwiperSlide>
              {/* Item 2 */}
              <SwiperSlide>
                <div className="movie-box-1">
                  <div className="poster"><img src="assets/images/posters/poster-6.jpg" alt="" /></div>
                  <div className="movie-details">
                    <h4 className="movie-title"><a href="#">Stranger Things</a></h4>
                    <span className="released">15 Jul 2016</span>
                  </div>
                </div>
              </SwiperSlide>
              {/* Item 3 */}
              <SwiperSlide>
                <div className="movie-box-1">
                  <div className="poster"><img src="assets/images/posters/poster-7.jpg" alt="" /></div>
                  <div className="movie-details">
                    <h4 className="movie-title"><a href="#">Luke Cage</a></h4>
                    <span className="released">30 Sep 2016</span>
                  </div>
                </div>
              </SwiperSlide>
              {/* Item 4 */}
              <SwiperSlide>
                <div className="movie-box-1">
                  <div className="poster"><img src="assets/images/posters/poster-8.jpg" alt="" /></div>
                  <div className="movie-details">
                    <h4 className="movie-title"><a href="#">The Flash</a></h4>
                    <span className="released">7 Oct 2014</span>
                  </div>
                </div>
              </SwiperSlide>
            </Swiper>
          </div>
        </div>
      </section>

      {/* 4. HOW IT WORKS (Đầy đủ 3 Step) */}
      <section className="how-it-works bg-light ptb100">
        <div className="container">
          <div className="row justify-content-center">
            <div className="col-md-7 text-center">
              <h2 className="title">How it works?</h2>
              <h6 className="subtitle">Step by step guide to start watching!</h6>
            </div>
          </div>
          <div className="timeline mt50">
            <span className="main-line"></span>
            
            {/* Step 1 */}
            <div className="timeline-step row">
              <span className="timeline-step-btn">Step 1</span>
              <div className="col-md-6 col-sm-12 timeline-text-wrapper">
                <div className="timeline-text">
                  <h3>Create an account</h3>
                  <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt.</p>
                </div>
              </div>
              <div className="col-md-6 col-sm-12 timeline-image-wrapper">
                <div className="timeline-image"><img src="assets/images/other/signup.png" alt="" /></div>
              </div>
            </div>

            {/* Step 2 */}
            <div className="timeline-step row">
              <span className="timeline-step-btn" style={{color: "#2a7bc2", background: "#c1ddf5"}}>Step 2</span>
              <div className="col-md-6 col-sm-12 timeline-image-wrapper">
                <div className="timeline-image"><img src="assets/images/other/pricing.png" alt="" /></div>
              </div>
              <div className="col-md-6 col-sm-12 timeline-text-wrapper">
                <div className="timeline-text-right">
                  <h3>Choose your Plan</h3>
                  <p>Choose the best plan that fits your needs and start streaming today.</p>
                </div>
              </div>
            </div>

            {/* Step 3 (Cái bạn đang cần đây!) */}
            <div className="timeline-step row">
              <span className="timeline-step-btn" style={{color: "#eb305f", background: "#f9c8d4"}}>Step 3</span>
              <div className="col-md-6 col-sm-12 timeline-text-wrapper">
                <div className="timeline-text">
                  <h3>Enjoy Movify</h3>
                  <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.</p>
                </div>
              </div>
              <div className="col-md-6 col-sm-12 timeline-image-wrapper">
                <div className="timeline-image"><img src="assets/images/other/enjoy-movify.png" alt="" /></div>
              </div>
            </div>
          </div>
        </div>
      </section>

     {/* 5. COUNTER SECTION */}
      <section className="counter bg-main-gradient ptb50 text-center">
        <div className="container">
          <div className="row text-white">
            {/* 1st Count up item */}
            <div className="col-md-4 col-sm-12">
              <span className="counter-item">964</span>
              <h4>Movies</h4>
            </div>

            {/* 2nd Count up item */}
            <div className="col-md-4 col-sm-12">
              <span className="counter-item">743</span>
              <h4>TV Shows</h4>
            </div>

            {/* 3rd Count up item */}
            <div className="col-md-4 col-sm-12">
              <span className="counter-item">1207</span>
              <h4>Users</h4>
            </div>
          </div>
        </div>
      </section>

    {/* 6. BECOME PREMIUM SECTION */}
      <section className="become-premium ptb100">
        <div className="container">
          <div className="row">
            <div className="col-md-5 col-sm-12 mb50">
              <h3 className="title">Become a Premium Member</h3>
              <h6 className="subtitle">
                Watch the latest movies and TV shows in HD and Ultra HD. 
                Join our community and enjoy unlimited content starting today!
              </h6>
            </div>

            <div className="col-md-7 col-sm-12">
              {/* Start of Pricing Table */}
              <div className="pricing-table-1">
                
                {/* Featured Holder (Danh sách tính năng bên trái) */}
                <div className="features-holder">
                  <div className="features-title">
                    <h5>What You Get</h5>
                  </div>
                  <div className="features-list-wrapper">
                    <ul className="features-list">
                      <li><h6>HD available</h6></li>
                      <li><h6>Ultra HD available</h6></li>
                      <li><h6>Unlimited Movies and TV shows</h6></li>
                      <li><h6>Watch on your phone & tablet</h6></li>
                      <li><h6>Download and watch offline</h6></li>
                      <li><h6>Screens you can watch</h6></li>
                      <li><h6>First Month Free</h6></li>
                    </ul>
                  </div>
                </div>

                {/* Price Table Featured (Cột Premium) */}
                <div className="price-table price-table-featured">
                  <div className="table-header">
                    <h5>Premium</h5>
                  </div>
                  <div className="table-content">
                    <ul>
                      <li><i className="fa fa-check"></i></li>
                      <li><i className="fa fa-check"></i></li>
                      <li><i className="fa fa-check"></i></li>
                      <li><i className="fa fa-check"></i></li>
                      <li><i className="fa fa-check"></i></li>
                      <li>5</li>
                      <li><i className="fa fa-check"></i></li>
                    </ul>
                  </div>
                  <div className="table-footer">
                    <div className="price-holder">
                      <span className="currency">$</span>
                      <span className="price">19.99</span>
                      <span className="time">/ mon</span>
                    </div>
                    <a href="#" className="btn btn-main btn-effect">
                      <i className="fa fa-shopping-cart"></i>
                    </a>
                  </div>
                </div>

                {/* Price Table Basic (Cột Basic) */}
                <div className="price-table">
                  <div className="table-header">
                    <h5>Basic</h5>
                  </div>
                  <div className="table-content">
                    <ul>
                      <li><i className="fa fa-check"></i></li>
                      <li><i className="fa fa-times"></i></li>
                      <li><i className="fa fa-check"></i></li>
                      <li><i className="fa fa-times"></i></li>
                      <li><i className="fa fa-times"></i></li>
                      <li>1</li>
                      <li><i className="fa fa-check"></i></li>
                    </ul>
                  </div>
                  <div className="table-footer">
                    <div className="price-holder">
                      <span className="currency">$</span>
                      <span className="price">9.99</span>
                      <span className="time">/ mon</span>
                    </div>
                    <a href="#" className="btn btn-main btn-effect">
                      <i className="fa fa-shopping-cart"></i>
                    </a>
                  </div>
                </div>

              </div>
              {/* End of Pricing Table */}
            </div>
          </div>
        </div>
      </section>

      {/* 7. BLOG (Đầy đủ 3 cột) */}
      <section className="blog bg-light ptb100">
        <div className="container">
          <div className="row justify-content-center text-center">
            <div className="col-md-7"><h2 className="title">Latest News</h2></div>
          </div>
          <div className="row mt50">
            {[1, 2, 3].map((item) => (
              <div className="col-md-4" key={item}>
                <div className="bloglist-post-holder shadow-hover">
                  <div className="bloglist-post-thumbnail" style={{background: `url(assets/images/blog/bloglist-${item}.jpg)`}}></div>
                  <div className="bloglist-text-wrapper">
                    <h4 className="bloglist-title"><a href="#">News Title {item}</a></h4>
                    <p>Sed ut perspiciatis unde omnis iste natus error sit voluptatem...</p>
                    <a href="#" className="btn-main">Read More</a>
                  </div>
                </div>
              </div>
            ))}
          </div>
        </div>
      </section>

      {/* 8. SUBSCRIBE */}
      <section className="subscribe bg-light2 ptb100 text-center">
        <div className="container">
          <h2 className="title">Join Movify Now!</h2>
          <div className="row justify-content-center mt50">
            <div className="col-md-6">
              <div className="input-group">
                <input type="email" className="form-control" placeholder="Your Email" />
                <button className="btn btn-main">Subscribe</button>
              </div>
            </div>
          </div>
        </div>
      </section>
    </>
  );
}

export default Home;