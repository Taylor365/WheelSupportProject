const uri = 'api/engineer';
let engineers = [];

function getEngineers() {
    fetch(uri)
        .then(response => response.json())
        .then(data => _displayEngineers(data))
        .catch(error => console.error('Unable to get engineers.', error));
}

function selectRandomEngineers() {
    const dayOfWork = document.getElementById('dayOfWork').value;
    const workDay = dayOfWork ? dayOfWork : new Date().toISOString().slice(0, 10);

    fetch(`${uri}/getRandomEngineers/${workDay}`)
        .then(response => response.json())
        .then(data => _displayRandomEngineers(data))
        .catch(error => console.error('Unable to get engineers.', error));
}

function updateEngineer(engineer) {
    const dayOfWork = document.getElementById('dayOfWork').value;
    const engineerToUpdate = {
        id: engineer.id,
        name: engineer.name,
        shiftCount: engineer.shiftCount + 1,
        lastWorked: dayOfWork ? dayOfWork : new Date().toISOString().slice(0, 10)
    };

    fetch(`${uri}/${engineer.id}`, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(engineerToUpdate)
    })
        .then(() => getEngineers())
        .catch(error => console.error('Unable to update engineer.', error));

    return false;
}

function _displayEngineers(data) {
    const tBody = document.getElementById('engineers');
    tBody.innerHTML = '';

    data.forEach(engineer => {
        let tr = tBody.insertRow();

        let td2 = tr.insertCell(0);
        let nameTextNode = document.createTextNode(engineer.name);
        td2.appendChild(nameTextNode);

        let td3 = tr.insertCell(1);
        let shiftTextNode = document.createTextNode(engineer.shiftCount);
        td3.appendChild(shiftTextNode);

        let td4 = tr.insertCell(2);
        let lastWorkedTextNode = document.createTextNode(engineer.lastWorked);
        td4.appendChild(lastWorkedTextNode);
    });

    engineers = data;
}

function _displayRandomEngineers(data) {
    const tBody = document.getElementById('randomEngineers');
    tBody.innerHTML = '';

    data.forEach(engineer => {
        let tr = tBody.insertRow();
        let td2 = tr.insertCell(0);
        let textNode = document.createTextNode(engineer.name);
        td2.appendChild(textNode);
        updateEngineer(engineer);
    });
}