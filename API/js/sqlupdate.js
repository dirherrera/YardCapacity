
$(function () {

	var update = $.connection.monitorHub;
	
	update.client.send = (nb, l) => {
		getData();
	}

	$.connection.hub.start().done(() => {
		getData();
	}).fail((error) => {
		console.log(error);
	});

});

function getData() {
	var param = new URLSearchParams(window.location.search)
	var yardid = param.get('yardid');
	
	$.ajax({
		url: '/Home/GetUpdate',
		data: {
			id: yardid
		},
		contentType: 'application/json; charset=utf-8',
		type: 'GET',
		dataType: 'json'
	}).done((res) => {
		UpdateMonitor(res);
	})
}

function UpdateMonitor(yard) {
	$("#yard").text(yard.Name);
	$("#max_equipment").text(yard.max_equipment);
	$("#cur_equipment").text(yard.cur_equipment);
	$("#max_units").text(yard.max_units);
	$("#cur_units").text(yard.cur_units);
	$("#update").text(yard.update.toDate().format());
	setTimeout(getData, 60000);
}