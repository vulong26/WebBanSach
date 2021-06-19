
var kichthuoc = document.getElementsByClassName("banner")[0].clientWidth;
var chuyenslide = document.getElementsByClassName("chuyen-slide")[0];
var img = chuyenslide.getElementsByTagName("img");
var max = kichthuoc * img.length;
max -= kichthuoc;
var chuyen = 0;
function Next() {
    if (chuyen < max) {
        chuyen += kichthuoc;
    }
    else
        chuyen = 0;
    
    chuyenslide.style.marginLeft = '-' + chuyen + 'px';
} 
function Back() {
    if (chuyen == 0) chuyen = max;
    else chuyen -= kichthuoc;
    chuyenslide.style.marginLeft = '-' + chuyen + 'px';
} 

setInterval(function () {
    Next();

}, 3000);