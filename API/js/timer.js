$(function () {
	CTime();
	var clock = setInterval(CTime, 1000);
});

function CTime() {
	var time = new Date();
	$("#clock").text(time.format());
}

Date.prototype.format = function() {
	var MMs = ["01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12"];
	var YY = this.getFullYear();
	var MM = MMs[this.getMonth()];
	var DD = this.getDate().DD();
	var hh = this.getHours().DD();
	var mm = this.getMinutes().DD();
	var ss = this.getSeconds().DD();
	var format = `${DD}/${MM}/${YY} ${hh}:${mm}:${ss}`;
	//console.log(format);
	return format;
}

String.prototype.toDate = function () {
	var re = /-?\d+/;
	var m = re.exec(this);
	console.log(m[0]);
	return new Date(parseInt(m[0]));
}

Number.prototype.DD = function () {
	return (this > 0 && this < 10) ? `0${this}` : `${this}`;
}