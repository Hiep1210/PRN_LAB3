"use strict";
var connection = new signalR.HubConnectionBuilder().withUrl("/carthub").build();

connection.start().then(
).catch(
    function (err) {
        return console.error(err.toString());
    }
);

connection.on("CartAdded", () => {
    alert("Added item to cart")
    location.reload();
});

