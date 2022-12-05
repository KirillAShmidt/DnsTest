async function elementUpdate(selector)
{
	try
	{
		var html = await (await fetch(location.href)).text();
		var newdoc = new DOMParser().parseFromString(html, 'text/html');

		document.querySelector(selector).outerHTML = newdoc.querySelector(selector).outerHTML;

		ShowRequestRange();

		return true;
	}
	catch (err)
	{
		console.log("Error");
		return false;
	}
}

function SendRequest()
{
	const request = new XMLHttpRequest();

	request.onload = () => {
		document.getElementById('output-field').innerHTML = request.responseText;

		elementUpdate('div#request-list');

		ShowRequestRange();

		console.log(requestList.length);
	}

	const requestData = `requestString=${form.requestString.value}`;

	if (form.requestString.value == "") {
		return;
	}

	let input = form.requestString;
	input.value = "";
	input.focus;
	input.select();


	request.open('post', 'PutInCommand');
	request.setRequestHeader('Content-type', 'application/x-www-form-urlencoded');
	request.send(requestData);
}

function GetOutput() {
	const request = new XMLHttpRequest();

	request.onload = () => {
		document.getElementById('output-field').innerHTML = request.responseText;
		//elementUpdate("div#output-field");
		console.log(request.responseText)
	}

	request.open('get', 'RecieveOutput');
	request.send();
}

function ShowRequestRange()
{
	requestList = document.getElementById('request-list').children;

	for (const item of requestList) {
		$(`div#${item.id}`).hide();
	}

	for (let i = counter; i < counter + amountOfRequests; i++) {
		$(`div#${requestList[i].id}`).show();
	}
}

document.addEventListener("DOMContentLoaded", () => {
	ShowRequestRange();
});


let requestList;
let counter = 0;
const amountOfRequests = 5;

const form =
{
	requestString: document.getElementById('requestString'),
	submit: document.getElementById('submit')
}

form.submit.addEventListener('click', () => {
	SendRequest();
});

document.addEventListener('keydown', (event) => {
	if (event.key == 'Enter') {
		event.preventDefault();
		SendRequest();
	}
});

document.addEventListener('keydown', (event) => {

	if (event.keyCode == '40' && counter + amountOfRequests < requestList.length) {
		counter++;
		ShowRequestRange();
	}
	if (event.keyCode == '38' && counter > 0) {
		counter--;
		ShowRequestRange();
	}

	preventDefault();
})

setInterval(GetOutput, 250);