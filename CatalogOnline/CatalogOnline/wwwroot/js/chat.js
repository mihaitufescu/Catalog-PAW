"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message, title) {
    var li = document.createElement("li");
    var userInfoElement = document.createElement("span"); // Creating a span element for user info (username, date, and time)
    var messageInfoElement = document.createElement("span"); // Creating a span element for the message info (title and message)

    // Getting the current date and time
    var currentDate = new Date();
    var formattedDate = currentDate.toLocaleDateString();
    var formattedTime = currentDate.toLocaleTimeString();

    // Setting the text content of the user info element (username, date, and time)
    userInfoElement.innerHTML = `<strong>${user}</strong> - ${formattedDate} ${formattedTime}`;

    // Setting the text content of the message info element (title and message)
    messageInfoElement.textContent = `${title} - ${message}`;

    // Appending the user info and message info elements to the list item
    li.appendChild(userInfoElement);
    li.appendChild(document.createElement("br")); // Adding a line break
    li.appendChild(messageInfoElement);

    // Appending the list item to the messages list
    document.getElementById("messagesList").appendChild(li);
});



connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    var title = document.getElementById("titleInput").value;
    connection.invoke("SendMessage", user, message, title).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});