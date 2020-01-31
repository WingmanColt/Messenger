(function($){
  "use strict";
  
  // Preloader 
	jQuery(window).on('load', function() {
		jQuery("#status").fadeOut();
		jQuery("#preloader").delay(350).fadeOut("slow");
	});
  
	// on ready function
	jQuery(document).ready(function($) {
	var $this = $(window);


	//show hide login form js
	$('#search_button').on("click", function(e) {
		$('#search_open').slideToggle();
		e.stopPropagation(); 
	});

	$(document).on("click", function(e){
		if(!(e.target.closest('#search_open'))){	
			$("#search_open").slideUp();   		
		}
   });
   
   
   
   
   // ===== Scroll to Top ==== 
$(window).scroll(function() {
    if ($(this).scrollTop() >= 100) {       
        $('#return-to-top').fadeIn(200);   
    } else {
        $('#return-to-top').fadeOut(200);  
    }
});
$('#return-to-top').on('click', function() {     
    $('body,html').animate({
        scrollTop : 0                
    }, 500);
});
   
   
      //------------------------ OWL JS Start --------------------//
   
   $(document).ready(function() {
              $('.jp_tittle_slider_content_wrapper .owl-carousel').owlCarousel({
                loop: true,
                margin: 10,
				autoplay:true,
                responsiveClass: true,
				navText : ['<i class="fa fa-chevron-left" aria-hidden="true"></i>','<i class="fa fa-chevron-right" aria-hidden="true"></i>'],
				animateOut: 'bounceInDown',
				animateIn: 'bounceInDown',
                responsive: {
                  0: {
                    items: 1,
                    nav: true
                  },
                  600: {
                    items: 1,
                    nav: true
                  },
                  1000: {
                    items: 1,
                    nav: true,
                    loop: true,
                    margin: 20
                  }
                }
              })
            })
			
			
			$(document).ready(function() {
              $('.jp_hiring_slider_wrapper .owl-carousel').owlCarousel({
                loop: true,
                margin: 10,
				autoplay:true,
                responsiveClass: true,
				smartSpeed: 1200,
				navText : ['<i class="fa fa-arrow-circle-o-left" aria-hidden="true"></i>','<i class="fa fa-arrow-circle-o-right" aria-hidden="true"></i>'],
                responsive: {
                  0: {
                    items: 1,
                    nav: true
                  },
                  600: {
                    items: 1,
                    nav: true
                  },
                  1000: {
                    items: 1,
                    nav: true,
                    loop: true,
                    margin: 20
                  }
                }
              })
            })
			
			$(document).ready(function() {
              $('.jp_career_slider_wrapper .owl-carousel').owlCarousel({
                loop: true,
                margin: 10,
				autoplay:true,
                responsiveClass: true,
				smartSpeed: 1200,
				navText : ['<i class="fa fa-arrow-circle-o-left" aria-hidden="true"></i>','<i class="fa fa-arrow-circle-o-right" aria-hidden="true"></i>'],
                responsive: {
                  0: {
                    items: 1,
                    nav: true
                  },
                  600: {
                    items: 2,
                    nav: true
                  },
                  1000: {
                    items: 3,
                    nav: true,
                    loop: true,
                    margin: 20
                  }
                }
              })
            })
			
			
			$(document).ready(function() {
              $('.jp_testi_slider_wrapper .owl-carousel').owlCarousel({
                loop: true,
                margin: 10,
				autoplay:true,
                responsiveClass: true,
				smartSpeed: 1200,
				navText : ['<i class="fa fa-arrow-circle-o-left" aria-hidden="true"></i>','<i class="fa fa-arrow-circle-o-right" aria-hidden="true"></i>'],
                responsive: {
                  0: {
                    items: 1,
                    nav: true
                  },
                  600: {
                    items: 1,
                    nav: true
                  },
                  1000: {
                    items: 1,
                    nav: true,
                    loop: true,
                    margin: 20
                  }
                }
              })
            })
			
			
			// Featured Products Js
				$('.ss_featured_products .owl-carousel').owlCarousel({
					loop:true,
					margin:0,
					nav:true,
					autoplay:true,
					smartSpeed: 1200,
					navText:["PREV" , "NEXT"],
					dots:true,
					responsive:{
						0:{
							items:1
						},
						600:{
							items:1
						},
						1000:{
							items:1
						}
					}
				});
				
				
				$(document).ready(function() {
              $('.jp_spotlight_slider_wrapper .owl-carousel').owlCarousel({
                loop: true,
                margin: 10,
				autoplay:true,
                responsiveClass: true,
				smartSpeed: 1200,
				navText : ['<i class="fa fa-arrow-circle-o-left" aria-hidden="true"></i>','<i class="fa fa-arrow-circle-o-right" aria-hidden="true"></i>'],
                responsive: {
                  0: {
                    items: 1,
                    nav: true
                  },
                  600: {
                    items: 1,
                    nav: true
                  },
                  1000: {
                    items: 1,
                    nav: true,
                    loop: true,
                    margin: 20
                  }
                }
              })
            })
			
			$(document).ready(function() {
              $('.jp_best_deal_slider_wrapper .owl-carousel').owlCarousel({
                loop: true,
                margin: 10,
				autoplay:true,
                responsiveClass: true,
				smartSpeed: 1200,
				navText : ['<i class="fa fa-arrow-circle-o-left" aria-hidden="true"></i>','<i class="fa fa-arrow-circle-o-right" aria-hidden="true"></i>'],
                responsive: {
                  0: {
                    items: 1,
                    nav: true
                  },
                  600: {
                    items: 1,
                    nav: true
                  },
                  1000: {
                    items: 1,
                    nav: true,
                    loop: true,
                    margin: 20
                  }
                }
              })
            })
			
			$(document).ready(function() {
              $('.jp_client_slider_wrapper .owl-carousel').owlCarousel({
                loop: true,
                margin: 10,
				autoplay:true,
                responsiveClass: true,
				smartSpeed: 1200,
				navText : ['<i class="fa fa-arrow-circle-o-left" aria-hidden="true"></i>','<i class="fa fa-arrow-circle-o-right" aria-hidden="true"></i>'],
                responsive: {
                  0: {
                    items: 1,
                    nav: true
                  },
                  600: {
                    items: 1,
                    nav: true
                  },
                  1000: {
                    items: 1,
                    nav: true,
                    loop: true,
                    margin: 20
                  }
                }
              })
            })
			
			
				$(document).ready(function() {
              $('.jp_first_blog_post_slider .owl-carousel').owlCarousel({
                loop: true,
                margin: 10,
				autoplay:true,
                responsiveClass: true,
				smartSpeed: 1200,
				navText : ['<i class="fa fa-arrow-circle-o-left" aria-hidden="true"></i>','<i class="fa fa-arrow-circle-o-right" aria-hidden="true"></i>'],
                responsive: {
                  0: {
                    items: 1,
                    nav: true
                  },
                  600: {
                    items: 1,
                    nav: true
                  },
                  1000: {
                    items: 1,
                    nav: true,
                    loop: true,
                    margin: 20
                  }
                }
              })
            })
			
			$(document).ready(function() {
              $('.jp_spotlight_slider_wrapper .owl-carousel').owlCarousel({
                loop: true,
                margin: 10,
				autoplay:true,
                responsiveClass: true,
				smartSpeed: 1200,
				navText : ['<i class="fa fa-arrow-circle-o-left" aria-hidden="true"></i>','<i class="fa fa-arrow-circle-o-right" aria-hidden="true"></i>'],
                responsive: {
                  0: {
                    items: 1,
                    nav: true
                  },
                  600: {
                    items: 1,
                    nav: true
                  },
                  1000: {
                    items: 1,
                    nav: true,
                    loop: true,
                    margin: 20
                  }
                }
              })
            })
			
			
			$(document).ready(function() {
              $('.jp_client_second_slider_wrapper .owl-carousel').owlCarousel({
                loop: true,
                margin: 10,
				autoplay:true,
                responsiveClass: true,
				smartSpeed: 1200,
				navText : ['<i class="fa fa-arrow-circle-o-left" aria-hidden="true"></i>','<i class="fa fa-arrow-circle-o-right" aria-hidden="true"></i>'],
                responsive: {
                  0: {
                    items: 1,
                    nav: true
                  },
                  600: {
                    items: 3,
                    nav: true
                  },
                  1000: {
                    items: 5,
                    nav: true,
                    loop: true,
                    margin: 20
                  }
                }
              })
            })
			
//------------------------ OWL JS End --------------------//
			
			
			
				//-------------------------------------------------------
    // counter-section
    //-------------------------------------------------------
    $('.jp_counter_main_wrapper').on('inview', function(event, visible, visiblePartX, visiblePartY) {
        if (visible) {
            $(this).find('.timer').each(function () {
                var $this = $(this);
                $({ Counter: 0 }).animate({ Counter: $this.text() }, {
                    duration: 2000,
                    easing: 'swing',
                    step: function () {
                        $this.text(Math.ceil(this.Counter));
                    }
                });
            });
            $(this).off('inview');
        }
    });
   
   
   
   
   $(document).ready(function() {
              $('.jp_listing_related_slider_wrapper .owl-carousel').owlCarousel({
                loop: true,
                margin: 10,
				autoplay:true,
                responsiveClass: true,
				smartSpeed: 1200,
				navText : ['PREV','NEXT'],
                responsive: {
                  0: {
                    items: 1,
                    nav: true
                  },
                  600: {
                    items: 1,
                    nav: true
                  },
                  1000: {
                    items: 1,
                    nav: true,
                    loop: true,
                    margin: 20
                  }
                }
              })
            })
			
			
			/*------------------OWL MAIN SLIDER-------------------------*/

 
  


	
				$(document).ready(function (){
			  var owl = $('.cc_slider_img_section .owl-carousel');
			  owl.owlCarousel({
				  loop:true,
				  margin:0,
				  autoplay:false,
				  navText : ['<i class="fa fa-angle-left" aria-hidden="true"></i>','<i class="fa fa-angle-right" aria-hidden="true"></i>'],
				  items:1
				  
			  });
			  


			  function setAnimation ( _elem, _InOut ) {
				var animationEndEvent = 'webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend';
				_elem.each ( function () {
				  var $elem = $(this);
				  var $animationType = 'animated ' + $elem.data( 'animation-' + _InOut );

				  $elem.addClass($animationType).one(animationEndEvent, function () {
					$elem.removeClass($animationType);
				  });
				});
			  }

			  owl.on('change.owl.carousel', function(event) {
				  var $currentItem = $('.owl-item', owl).eq(event.item.index);
				  var $elemsToanim = $currentItem.find("[data-animation-out]");
				  setAnimation ($elemsToanim, 'out');
			  });

			  owl.on('changed.owl.carousel', function(event) {

				  var $currentItem = $('.owl-item', owl).eq(event.item.index);
				  var $elemsToanim = $currentItem.find("[data-animation-in]");
				  setAnimation ($elemsToanim, 'in');
			  })

			});

   
   
	
	});
})();   