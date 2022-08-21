document.addEventListener("DOMContentLoaded", () => {
    const input = document.getElementById("textbox");
    const connectButton = document.getElementById("connect");
    const disconnectButton = document.getElementById("disconnect");
    const sendButton = document.getElementById("send");
    const status = document.getElementById("status");

    let ws;

    connectButton.addEventListener("click", async () => {
        let res = await fetch("http://localhost:5231/api/auth", { method: "POST" });

        status.textContent = "Connecting...";

        let data = await res.json();

        ws = new WebSocket("wss://ws.quicksocket.io/?t=" + data.connectionToken);

        ws.addEventListener("open", function () {
            updateView("connected");
        })

        ws.addEventListener("message", function (event) {
            updateView("message", event.data);
        });

        ws.addEventListener("close", function () {
            updateView("disconnect");
        });
    })

    sendButton.addEventListener("click", async () => {
        if (input.value === "") return;

        ws.send(input.value);
    })

    disconnectButton.addEventListener("click", async () => {
        ws.close();
    })
})

function updateView(update, message) {
    const input = document.getElementById("textbox");
    const status = document.getElementById("status");
    const whiteboard = document.getElementById("whiteboard");

    switch(update) {
        case "connected":
            input.removeAttribute("disabled");
            status.textContent = "Connected";
            break;
        case "message":
            whiteboard.textContent = message;
            break;
        case "disconnect":
            input.setAttribute("disabled", "disabled");
            status.textContent = "Disconnected";
            break;
    }
}

