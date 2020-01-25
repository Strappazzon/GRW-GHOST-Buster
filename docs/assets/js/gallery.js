---
---

// http://www.new2html.com/tutorial/using-javascript-change-image-src-attribute/
function changeScr(scrName, scrAlt) {
	let scr = document.getElementById('scr');
	let scra = document.getElementById('scr-a');
	scr.src = "{{ '/assets/img/' | relative_url }}" + scrName;
	scra.href = "{{ '/assets/img/' | relative_url }}" + scrName;
	scr.alt = scrAlt
}
