$(function () {
	CTime();
	var clock = setInterval(CTime, 1000);
});

function CTime() {
	var time = new Date();
	$("#clock").text(DFormat(time));
}

function DFormat(d) {
	var MMs = ["01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12"];
	var YY = d.getFullYear();
	var MM = MMs[d.getMonth()];
	var DD = d.getDate().DD();
	var hh = d.getHours().DD();
	var mm = d.getMinutes().DD();
	var ss = d.getSeconds().DD();
	var format = `${MM}/${DD}/${YY} ${hh}:${mm}:${ss}`;
	//console.log(format);
	return format;
}
Number.prototype.DD = function () {
	return (this > 0 && this < 10) ? `0${this}` : `${this}`;
}