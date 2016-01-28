$(function () {
    var $carousel = $('#carousel'),
		$pager = $('#pager');

    //	gather the thumbnails
    var $thumb = $('<div class="thumb" />');
    $carousel.children().each(function () {
        var src = $(this).attr('src');
        $thumb.append('<img src="' + src.split('/large/').join('/small/') + '" />');
    });

    //	duplicate the thumbnails
    for (var a = 0; a < $carousel.children().length - 1; a++) {
        $pager.append($thumb.clone());
    }

    //	create large carousel
    $carousel.carouFredSel({
        items: {
            visible: 1,
            width: 550,
            height: 310
        },
        scroll: {
            fx: 'directscroll',
            onBefore: function (data) {
                var oldSrc = data.items.old.attr('src').split('/large/').join('/small/'),
					newSrc = data.items.visible.attr('src').split('/large/').join('/small/'),
					$t = $thumbs.find('img:first-child[src="' + newSrc + '"]').parent();

                $t.trigger('slideTo', [$('img[src="' + oldSrc + '"]', $t), 'next']);
            }
        }
    });

    //	create thumb carousels
    var $thumbs = $('.thumb');
    $thumbs.each(function (i) {
        $(this).carouFredSel({
            auto: false,
            scroll: {
                fx: 'directscroll'
            },
            items: {
                start: i + 1,
                visible: 1,
                width: 100,
                height: 80
            }
        });

        //	click the carousel
        $(this).click(function () {
            var src = $(this).children().first().attr('src').split('/small/').join('/large/');
            $carousel.trigger('slideTo', [$('img[src="' + src + '"]', $carousel), 'next']);
        });
    });
});