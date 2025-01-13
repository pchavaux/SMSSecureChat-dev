let connection;
let peerConnection;

async function startSignalRConnection() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("/webrtchub")
        .build();

    await connection.start();
    console.log("SignalR Connected");

    // SignalR handlers
    connection.on("ReceiveOffer", async (offer) => {
        console.log("Offer received", offer);
        await peerConnection.setRemoteDescription({ type: "offer", sdp: offer });
        const answer = await peerConnection.createAnswer();
        await peerConnection.setLocalDescription(answer);

        connection.invoke("SendAnswer", answer.sdp);
    });

    connection.on("ReceiveAnswer", async (answer) => {
        console.log("Answer received", answer);
        await peerConnection.setRemoteDescription({ type: "answer", sdp: answer });
    });

    connection.on("ReceiveCandidate", (candidate) => {
        console.log("Candidate received", candidate);
        peerConnection.addIceCandidate(new RTCIceCandidate(candidate));
    });
}

async function createPeerConnection() {
    peerConnection = new RTCPeerConnection({
        iceServers: [{ urls: "stun:stun.l.google.com:19302" }]
    });

    peerConnection.onicecandidate = (event) => {
        if (event.candidate) {
            connection.invoke("SendCandidate", event.candidate.candidate);
        }
    };

    peerConnection.ontrack = (event) => {
        const remoteVideo = document.getElementById("remoteVideo");
        remoteVideo.srcObject = event.streams[0];
    };

    console.log("PeerConnection created");
}

async function makeCall() {
    const offer = await peerConnection.createOffer();
    await peerConnection.setLocalDescription(offer);
    connection.invoke("SendOffer", offer.sdp);
}

// Initialize
async function initialize() {
    await startSignalRConnection();
    await createPeerConnection();
}
async function addLocalStreamToPeerConnection() {
    localStream.getTracks().forEach(track => {
        peerConnection.addTrack(track, localStream);
    });
}
// Start initialization
initialize();
