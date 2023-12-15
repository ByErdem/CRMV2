$(() => {


    var $wrapper = $('.main-wrapper');
    var $slimScrolls = $('.slimscroll');


	// Mobile menu sidebar overlay
	$('body').append('<div class="sidebar-overlay"></div>');
	$(document).on('click', '#mobile_btn', function () {
		$wrapper.toggleClass('slide-nav');
		$('.sidebar-overlay').toggleClass('opened');
		$('html').addClass('menu-opened');
		$('#task_window').removeClass('opened');
		return false;
	});

	$(".sidebar-overlay").on("click", function () {
		$('html').removeClass('menu-opened');
		$(this).removeClass('opened');
		$wrapper.removeClass('slide-nav');
		$('.sidebar-overlay').removeClass('opened');
		$('#task_window').removeClass('opened');
	});

	// Sidebar Slimscroll
	if ($slimScrolls.length > 0) {
		$slimScrolls.slimScroll({
			height: 'auto',
			width: '100%',
			position: 'right',
			size: '7px',
			color: '#ccc',
			wheelStep: 10,
			touchScrollStep: 100
		});
		var wHeight = $(window).height() - 60;
		$slimScrolls.height(wHeight);
		$('.sidebar .slimScrollDiv').height(wHeight);
		$(window).resize(function () {
			var rHeight = $(window).height() - 60;
			$slimScrolls.height(rHeight);
			$('.sidebar .slimScrollDiv').height(rHeight);
		});
	}

	// Sidebar
	var Sidemenu = function () {
		this.$menuItem = $('#sidebar-menu a');
	};

	function init() {
		var $this = Sidemenu;
		$('#sidebar-menu a').on('click', function (e) {
			if ($(this).parent().hasClass('submenu')) {
				e.preventDefault();
			}
			if (!$(this).hasClass('subdrop')) {
				// $('ul', $(this).parents('ul:first')).slideUp(250);
				$('a', $(this).parents('ul:first')).removeClass('subdrop');
				$(this).next('ul').slideDown(350);
				$(this).addClass('subdrop');
			} else if ($(this).hasClass('subdrop')) {
				$(this).removeClass('subdrop');
				$(this).next('ul').slideUp(350);
			}
		});
		$('#sidebar-menu ul li.submenu a.active').parents('li:last').children('a:first').addClass('active').trigger('click');
	}

	// Sidebar Initiate
	init();
	$(document).on('mouseover', function (e) {
		e.stopPropagation();
		if ($('body').hasClass('mini-sidebar') && $('#toggle_btn').is(':visible')) {
			var targ = $(e.target).closest('.sidebar, .header-left').length;
			if (targ) {
				$('body').addClass('expand-menu');
				$('.subdrop + ul').slideDown();
			} else {
				$('body').removeClass('expand-menu');
				$('.subdrop + ul').slideUp();
			}
			return false;
		}
	});

});


$(document).on('mouseover', function (e) {
	e.stopPropagation();
	if ($('body').hasClass('mini-sidebar') && $('#toggle_btn').is(':visible')) {
		var targ = $(e.target).closest('.sidebar, .header-left').length;
		if (targ) {
			$('body').addClass('expand-menu');
			$('.subdrop + ul').slideDown();
		} else {
			$('body').removeClass('expand-menu');
			$('.subdrop + ul').slideUp();
		}
		return false;
	}
});

//toggle_btn
$(document).on('click', '#toggle_btn', function () {
	if ($('body').hasClass('mini-sidebar')) {
		$('body').removeClass('mini-sidebar');
		$(this).addClass('active');
		$('.subdrop + ul');
		localStorage.setItem('screenModeNightTokenState', 'night');
		setTimeout(function () {
			$("body").removeClass("mini-sidebar");
			$(".header-left").addClass("active");
		}, 100);
	} else {
		$('body').addClass('mini-sidebar');
		$(this).removeClass('active');
		$('.subdrop + ul');
		localStorage.removeItem('screenModeNightTokenState', 'night');
		setTimeout(function () {
			$("body").addClass("mini-sidebar");
			$(".header-left").removeClass("active");
		}, 100);
	}
	return false;
});