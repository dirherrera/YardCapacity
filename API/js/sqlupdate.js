
$(function () {

	var update = $.connection.monitorHub;
	console.log(update);
	update.client.updateMessages = (nb, l) => {
		getData();
		console.log(nb);
	}

	$.connection.hub.start().done(() => {
		console.log('connection start');
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
		var json = res;
		console.log(json);
	})
}
