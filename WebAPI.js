"use strict";

window.WebAPI = {
	buildFullUrl: function (url, args, start) {
		var name, value, i, fullUrl = url + "?";
		for (i = start; i < args.length; i += 2) {
			if (i !== start) {
				fullUrl += "&";
			}
			name = args[i];
			value = args[i + 1];
			if (name === undefined || name === null || value === undefined || value === null) {
				throw "Argumentos inválidos";
			}
			name = name.toString();
			value = value.toString();
			if (!name.length || !value.length) {
				throw "Argumentos inválidos";
			}
			fullUrl += encodeURIComponent(name) + "=" + encodeURIComponent(value);
		}
		return fullUrl;
	},
	get: function (url, name0, value0) {
		if (!url || !url.length) {
			throw "URL inválido";
		}

		if (!(arguments.length & 1)) {
			throw "Quantidade de argumentos inválidos";
		}

		var i, req, fullUrl, name, value;

		if (arguments.length > 1) {
			fullUrl = WebAPI.buildFullUrl(url, arguments, 1);
		} else {
			fullUrl = url;
		}

		// Cria uma requisição AJAX
		req = new XMLHttpRequest();

		// Abre a requisição com o método HTTP GET

		// *** A requisição está sendo aberta em modo síncrono, para facilitar
		// a compreensão! Em um sistema real, convém utilizar o modo assíncrono!
		req.open("get", fullUrl, false);

		// Pede para o servidor devolver dados em JSON
		req.setRequestHeader("Accept", "application/json");

		// Envia a requisição sincronamente
		req.send();

		// Faz o condicional dessa forma, pois pode ser que recebamos
		// um status 204 (No Content)
		if (req.status === 200) {
			// Tudo correu bem
			return { status: req.status, value: JSON.parse(req.responseText) };
		} else if (req.status === 204) {
			// Tudo correu bem
			return { status: req.status, value: "" };
		} else {
			// Erros devem ser tratados aqui!!!
			return { status: req.status, value: null };
		}
	},
	getAsync: function (url, callback, name0, value0) {
		if (!url || !url.length) {
			throw "URL inválido";
		}

		if (!callback) {
			throw "Callback inválido";
		}

		if ((arguments.length & 1)) {
			throw "Quantidade de argumentos inválidos";
		}

		var i, req, fullUrl, name, value, done = false;

		if (arguments.length > 1) {
			fullUrl = WebAPI.buildFullUrl(url, arguments, 1);
		} else {
			fullUrl = url;
		}

		// Cria uma requisição AJAX
		req = new XMLHttpRequest();

		// Abre a requisição com o método HTTP GET

		// *** A requisição está sendo aberta em modo síncrono, para facilitar
		// a compreensão! Em um sistema real, convém utilizar o modo assíncrono!
		req.open("get", fullUrl, true);

		// Pede para o servidor devolver dados em JSON
		req.setRequestHeader("Accept", "application/json");

		// Configura o callback assíncrono
		req.onreadystatechange = function () {
			if (req.readyState === 4 && !done) {
				done = true;
				// Faz o condicional dessa forma, pois pode ser que recebamos
				// um status 204 (No Content)
				if (req.status === 200) {
					// Tudo correu bem
					callback({ status: req.status, value: JSON.parse(req.responseText) });
				} else if (req.status === 204) {
					// Tudo correu bem
					callback({ status: req.status, value: "" });
				} else {
					// Erros devem ser tratados aqui!!!
					callback({ status: req.status, value: null })
				}
			}
		}

		// Envia a requisição assincronamente
		req.send();
	},
	post: function (url, bodyObject, name0, value0) {
		if (!url || !url.length) {
			throw "URL inválido";
		}

		if (!bodyObject) {
			throw "Corpo inválido";
		}

		if ((arguments.length & 1)) {
			throw "Quantidade de argumentos inválidos";
		}

		var i, req, fullUrl, name, value;

		if (arguments.length > 2) {
			fullUrl = WebAPI.buildFullUrl(url, arguments, 2);
		} else {
			fullUrl = url;
		}

		// Cria uma requisição AJAX
		req = new XMLHttpRequest();

		// Abre a requisição com o método HTTP POST

		// *** A requisição está sendo aberta em modo síncrono, para facilitar
		// a compreensão! Em um sistema real, convém utilizar o modo assíncrono!
		req.open("post", fullUrl, false);

		// Configura a requisição para enviar dados JSON através do corpo
		// da mensagem (por onde será enviado o objeto pessoa)
		req.setRequestHeader("Content-type", "application/json; charset=utf-8");

		// Pede para o servidor devolver dados em JSON
		req.setRequestHeader("Accept", "application/json");

		// Envia a requisição sincronamente
		req.send(JSON.stringify(bodyObject));

		// Faz o condicional dessa forma, pois pode ser que recebamos
		// um status 204 (No Content)
		if (req.status === 200) {
			// Tudo correu bem
			return { status: req.status, value: JSON.parse(req.responseText) };
		} else if (req.status === 204) {
			// Tudo correu bem
			return { status: req.status, value: "" };
		} else {
			// Erros devem ser tratados aqui!!!
			return { status: req.status, value: null };
		}
	},
	postAsync: function (url, bodyObject, callback, name0, value0) {
		if (!url || !url.length) {
			throw "URL inválido";
		}

		if (!bodyObject) {
			throw "Corpo inválido";
		}

		if (!callback) {
			throw "Callback inválido";
		}

		if (!(arguments.length & 1)) {
			throw "Quantidade de argumentos inválidos";
		}

		var i, req, fullUrl, name, value, done;

		if (arguments.length > 2) {
			fullUrl = WebAPI.buildFullUrl(url, arguments, 2);
		} else {
			fullUrl = url;
		}

		// Cria uma requisição AJAX
		req = new XMLHttpRequest();

		// Abre a requisição com o método HTTP POST

		// *** A requisição está sendo aberta em modo síncrono, para facilitar
		// a compreensão! Em um sistema real, convém utilizar o modo assíncrono!
		req.open("post", fullUrl, true);

		// Configura a requisição para enviar dados JSON através do corpo
		// da mensagem (por onde será enviado o objeto pessoa)
		req.setRequestHeader("Content-type", "application/json; charset=utf-8");

		// Pede para o servidor devolver dados em JSON
		req.setRequestHeader("Accept", "application/json");

		// Configura o callback assíncrono
		req.onreadystatechange = function () {
			if (req.readyState === 4 && !done) {
				done = true;
				// Faz o condicional dessa forma, pois pode ser que recebamos
				// um status 204 (No Content)
				if (req.status === 200) {
					// Tudo correu bem
					callback({ status: req.status, value: JSON.parse(req.responseText) });
				} else if (req.status === 204) {
					// Tudo correu bem
					callback({ status: req.status, value: "" });
				} else {
					// Erros devem ser tratados aqui!!!
					callback({ status: req.status, value: null })
				}
			}
		}

		// Envia a requisição sincronamente
		req.send(JSON.stringify(bodyObject));
	}
};
