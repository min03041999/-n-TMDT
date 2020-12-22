/*---upload img--*/
function ShowImagePreview(imageUploader, previewImage) {
	if (imageUploader.files && imageUploader.files[0]) {
		var reader = new FileReader();
		reader.onload = function (e) {
			$(previewImage).attr('src', e.target.result);
		}
		reader.readAsDataURL(imageUploader.files[0]);
	}
}

function ShowImagePreview2(imageUploader, previewImage2) {
	if (imageUploader.files && imageUploader.files[0]) {
		var reader = new FileReader();
		reader.onload = function (e) {
			$(previewImage2).attr('src', e.target.result);
		}
		reader.readAsDataURL(imageUploader.files[0]);
	}
}
function ShowImagePreview3(imageUploader, previewImage3) {
	if (imageUploader.files && imageUploader.files[0]) {
		var reader = new FileReader();
		reader.onload = function (e) {
			$(previewImage3).attr('src', e.target.result);
		}
		reader.readAsDataURL(imageUploader.files[0]);
	}
}
function ShowImagePreview4(imageUploader, previewImage4) {
	if (imageUploader.files && imageUploader.files[0]) {
		var reader = new FileReader();
		reader.onload = function (e) {
			$(previewImage4).attr('src', e.target.result);
		}
		reader.readAsDataURL(imageUploader.files[0]);
	}
}
function ShowImagePreview5(imageUploader, previewImage5) {
	if (imageUploader.files && imageUploader.files[0]) {
		var reader = new FileReader();
		reader.onload = function (e) {
			$(previewImage5).attr('src', e.target.result);
		}
		reader.readAsDataURL(imageUploader.files[0]);
	}
}
function ShowImagePreview6(imageUploader, previewImage6) {
	if (imageUploader.files && imageUploader.files[0]) {
		var reader = new FileReader();
		reader.onload = function (e) {
			$(previewImage6).attr('src', e.target.result);
		}
		reader.readAsDataURL(imageUploader.files[0]);
	}
}

function addtocart() {
	var x = document.getElementById("snackbar");
	x.className = "show";
	setTimeout(function () { x.className = x.className.replace("show", ""); }, 3000);
}