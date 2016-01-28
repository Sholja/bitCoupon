

$(document).ready(function () {

    $("#couponPlace").on("mouseenter mouseleave", "div.blog-post", function () {
        
            $(this).find("div.content-hide").slideToggle("fast");
    });

 /* $("div.blog-post").hover(
    function() {
        $(this).find("div.content-hide").slideToggle("fast");
    },
    function() {
        $(this).find("div.content-hide").slideToggle("fast");
    }
  );*/

  $('.flexslider').flexslider({
		prevText: '',
		nextText: ''
	});

  $('.testimonails-slider').flexslider({
    animation: 'slide',
    slideshowSpeed: 5000,
    prevText: '',
    nextText: '',
    controlNav: false
  });

  $(function(){

  // Instantiate MixItUp:

  $('#Container').mixItUp();

  

  $(document).ready(function() {
      $(".fancybox").fancybox();
    });

  });
  
 

});

//function reloadJs(src) {
//    src = $('script[src$="' + src + '"]').attr("src");
//    console.log(src);
//    $('script[src$="' + src + '"]').remove();
//    $('<script/>').attr('src', src).appendTo('body');
//}

//$("#pagination").click(function () {

//    reloadJs("/Scripts/main.js");



//});

