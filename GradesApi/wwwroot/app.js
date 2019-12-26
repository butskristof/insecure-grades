addEventListener('load', init, false);
let testData = [];

function init() {
	document.querySelector("#btn-login")
		.addEventListener('click', login, false);
	document.querySelector("#btn-get-score")
		.addEventListener('click', getScore, false);
	document.querySelector("#btn-update-score")
		.addEventListener('click', updateScore, false);
}

function login() {
	console.log("Authorising as student");
	fetch("/api/users/gettoken", {
		method: "POST",
		headers: {
			'Accept': 'application/json',
			'Content-Type': 'application/json'
		},
		body: JSON.stringify({
			id: "cea4e519-8411-4606-9908-8c0bf28525a0",
			password: "student"
		})
	})
		.then(result => result.json())
		.then(data => {
			const token = data.token;
			localStorage.setItem("token", token);
		})
		.catch(err => console.error(err));
}

function getScore() {
	console.log("Getting test score");
	
	fetch("/api/tests/cea4e519-8411-4606-9908-8c0bf28525a0")
		.then(result => result.json())
		.then(data => {
			testData = data;
			clearContent("#get-response");
			data.forEach(t => {
				appendMessage("#get-response", `${t.score}/${t.maxScore}`);
			})
		})
		.catch(err => console.error(err));
}

function updateScore() {
	console.log("Trying to update score");
	const token = localStorage.getItem("token");

	if (!token) {
		showMessage("#update-response", "Please log in first.")
	}
	
	if (!testData.length) {
		showMessage("#update-response", "Please view test scores first.")
	}
	
	testData.forEach(t => {
		fetch(`/api/tests/${t.id}`, {
			method: "PUT",
			headers: {
				'Accept': 'application/json',
				'Content-Type': 'application/json',
				'Authorization': `Bearer ${token}`
			},
			body: JSON.stringify({
				score: 10
			})
		})
			.then(result => {
				console.log(result);
				if (result.status === 401) {
					showMessage("#update-response", "Unauthorized")
				} else {
					showMessage("#update-response", "Success!")
				}
			})
	});
}

function showMessage(selector, message) {
	clearContent(selector);
	document.querySelector(selector).textContent = message;
}

function appendMessage(selector, message) {
	document.querySelector(selector).textContent += `\n${message}`;
}

function clearContent(selector) {
	document.querySelector(selector).textContent = "";
}
