function closeAllAutocompleteLists() {
    var x = document.getElementsByClassName('autocomplete-items');
    for (var i = 0; i < x.length; i++) {
        x[i].parentNode.removeChild(x[i]);
    }
}

//Finds y value of given object
function findPos(obj) {
    var curtop = 0;
    if (obj.offsetParent) {
        do {
            curtop += obj.offsetTop;
        } while (obj = obj.offsetParent);
        return [curtop];
    }
}

function setAutocomplete(inp, arr) {
    window.scroll(0, findPos(inp));
    var a, b, i;
    closeAllAutocompleteLists();
    a = document.createElement('div');
    a.setAttribute('id', this.id + 'autocomplete-list');
    a.setAttribute('class', 'autocomplete-items');
    inp.parentNode.appendChild(a);
    for (i = 0; i < arr.length; i++) {
        b = document.createElement('div');
        b.innerHTML = arr[i];
        b.addEventListener('click', function (e) {
            inp.value = e.target.innerText;
            closeAllAutocompleteLists();
            $('.modal').modal('show');
            modalBody.innerHTML = '';
            var spinnerDiv = document.createElement('div');
            spinnerDiv.classList.add('spinner-border');
            spinnerDiv.classList.add('text-alert');
            spinnerDiv.classList.add('d-flex');
            spinnerDiv.classList.add('mt-2');
            spinnerDiv.classList.add('ml-auto');
            spinnerDiv.classList.add('mr-auto');
            spinnerDiv.setAttribute('role', 'status');
            var span = document.createElement('span');
            span.classList.add('sr-only');
            span.innerText = 'Loading...';
            spinnerDiv.appendChild(span);
            modalBody.appendChild(spinnerDiv);
            getFoodByName(e.target.innerText);
        });
        a.appendChild(b);
    }
}

function getFoodNames(search) {

    return fetch('/index?handler=NameSearch&searchString=' + search,
        {
            method: 'get',
            headers: {
                'Content-Type': 'application/json;charset=UTF-8'
            }
        })
        .then(function (response) {
            if (response.ok) {
                return response.text();
            } else {
                throw Error('FoodNameSearch Response Not OK');
            }
        })
        .then(function (text) {
            try {
                return JSON.parse(text);
            } catch (err) {
                throw Error('FoodNameSearch Method Not Found');
            }
        })
        .then(function (responseJSON) {
            setAutocomplete(document.querySelector('#GetNameInputId'), responseJSON);
        })
        .catch(function (error) {
            modalBody.innerHTML = '';
            var alertDiv = document.createElement('div');
            alertDiv.classList.add('alert', 'alert-danger');
            alertDiv.textContent = 'Error = ' + error;
            modalBody.appendChild(alertDiv);
        });
}

function getFoodByName(name) {

    return fetch('/index?handler=FoodByName&name=' + name,
        {
            method: 'get',
            headers: {
                'Content-Type': 'application/json;charset=UTF-8'
            }
        })
        .then(function (response) {
            if (response.ok) {
                return response.text();
            } else {
                throw Error('FoodByName Response Not OK');
            }
        })
        .then(function (text) {
            try {
                return JSON.parse(text);
            } catch (err) {
                throw Error('FoodByName Method Not Found');
            }
        })
        .then(function (responseJSON) {
            var dl, dt, dd;
            modalBody.innerHTML = '';
            if (responseJSON.Status === 'Success') {
                dl = document.createElement('dl');
                for (prop in responseJSON) {
                    dt = document.createElement('dt');
                    dt.textContent = prop;
                    dl.appendChild(dt);
                    dd = document.createElement('dd');
                    dd.textContent = responseJSON[prop].length === 0 ? 'empty' : responseJSON[prop];
                    dl.appendChild(dd);
                }
                var successDiv = document.createElement('div');
                successDiv.classList.add('alert', 'alert-success');
                successDiv.appendChild(dl);
                modalBody.appendChild(successDiv);
            }
            else {
                dl = document.createElement('dl');
                for (prop in responseJSON) {
                    dt = document.createElement('dt');
                    dt.textContent = prop;
                    dl.appendChild(dt);
                    dd = document.createElement('dd');
                    dd.textContent = responseJSON[prop];
                    dl.appendChild(dd);
                }
                var alertDiv = document.createElement('div');
                alertDiv.classList.add('alert', 'alert-danger');
                alertDiv.appendChild(dl);
                modalBody.appendChild(alertDiv);
            }
        })
        .catch(function (error) {
            modalBody.innerHTML = '';
            var alertDiv = document.createElement('div');
            alertDiv.classList.add('alert', 'alert-danger');
            alertDiv.textContent = 'Error ' + error;
            modalBody.appendChild(alertDiv);
        });
}


// Wait for the page to load first
document.addEventListener('DOMContentLoaded', function () {

    /* close autocompletes when someone clicks in the document:*/
    document.addEventListener('click', function (e) {
        closeAllAutocompleteLists();
    });

    if (document.querySelector('#GetNameInputId')) {
        document.querySelector('#GetNameInputId').addEventListener('keyup', function (e) {
            if (e.target.value.length === 0) {
                closeAllAutocompleteLists();
            } else {
                getFoodNames(e.target.value);
            }
        });
    }

    if (document.querySelector('#GetFoodByNameButtonId')) {
        document.querySelector('#GetFoodByNameButtonId').addEventListener('click', function () {
            var nameInput = document.querySelector('#GetNameInputId');
            if (nameInput.value.length === 0) {
                modalBody.innerHTML = '';
                var alertDiv = document.createElement('div');
                alertDiv.classList.add('alert', 'alert-danger');
                alertDiv.textContent = 'Error - Name is required.';
                modalBody.appendChild(alertDiv);
                return;
            }
            modalBody.innerHTML = '';
            var spinnerDiv = document.createElement('div');
            spinnerDiv.classList.add('spinner-border');
            spinnerDiv.classList.add('text-alert');
            spinnerDiv.classList.add('d-flex');
            spinnerDiv.classList.add('mt-2');
            spinnerDiv.classList.add('ml-auto');
            spinnerDiv.classList.add('mr-auto');
            spinnerDiv.setAttribute('role', 'status');
            var span = document.createElement('span');
            span.classList.add('sr-only');
            span.innerText = 'Loading...';
            spinnerDiv.appendChild(span);
            modalBody.appendChild(spinnerDiv);
            getFoodByName(nameInput.value);
        });
    }

});