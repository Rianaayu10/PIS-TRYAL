/*
Template Name: Color Admin - Responsive Admin Dashboard Template build with Twitter Bootstrap 5
Version: 5.2.0
Author: Sean Ngu
Website: http://www.seantheme.com/color-admin/
*/

import PhotoSwipeLightbox from '../../lib/photoswipe/dist/photoswipe-lightbox.esm.js';
const lightbox = new PhotoSwipeLightbox({
	gallery: '#gallery',
	children: 'a',
	pswpModule: () => import('../../lib/photoswipe/dist/photoswipe.esm.js')
});
lightbox.init();