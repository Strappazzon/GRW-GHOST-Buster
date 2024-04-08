// SCREENSHOTS GALLERY
// Change screenshot when clicking the button on the left

function changeScr(fName, aText) {
  let elImg = document.getElementById('screenshot');

  elImg.src = './assets/img/screenshot/' + fName;
  elImg.alt = aText;
}
