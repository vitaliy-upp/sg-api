var nolimitServerUrl = "";
nolimitServerUrl = window.location.origin;
//nolimitServerUrl = "https://localhost:44350";
//nolimitServerUrl = "https://vsummitapp-webapi.azurewebsites.net";
//nolimitServerUrl = "https://demo-api.vsummits.io";
//nolimitServerUrl = "https://qa-api.vsummits.io";

function BodyOnLoad() {
    let accesstoken = getSession("access_token");
    if (accesstoken) {
        HideAccountShowOther();
        addListeners();
    } else {
        ShowAccountHideOther();
    }
};

function ShowAccountHideOther() {
    document.getElementById("row-sign-up-in").style.display = "flex";

    document.getElementById("row-event-space").style.display = "none";
}
function HideAccountShowOther() {
    document.getElementById("row-sign-up-in").style.display = "none";

    document.getElementById("row-event-space").style.display = "flex";
}
// WORK WITH SESSION/COOKIES

function setSession(key, value) {
    window.sessionStorage.setItem(key, value);
}
function getSession(key) {
    return window.sessionStorage.getItem(key);
}

// SEND REQUEST 

async function Send(url, method, data) {
    let options = {
        method: method
    };
    if (method != "GET") {
        options.headers = { "Content-Type": "application/json" };
        options.body = JSON.stringify(data);
    }

    url = nolimitServerUrl + url;
    // send request
    let response = await fetch(url, options);
    return response;
}
async function SendWithAuth(url, method, data) {
    let options = {
        method: method,
        headers: {
            "Authorization": "Bearer " + getSession("access_token")
        }
    };
    if (data && method != "GET") {
        options.headers["Content-Type"] = "application/json";
        options.body = JSON.stringify(data);
    }

    url = nolimitServerUrl + url;
    // отправляет запрос и получаем ответ
    let response = await fetch(url, options);
    return response;
}
async function LoadData(url) {
    let opts = {
        method: "GET"
    };
    url = nolimitServerUrl + url;
    // send request
    let response = await fetch(url, opts);
    return response;
}

// SIGN UP / SIGN IN

async function Login(form) {
    // getting form data
    var formData = {
        "username": form["txtUsername"].value,
        "password": form["txtPassword"].value
    }
    var resp = await Send("/api/account/token", "POST", formData);
    if (resp.ok) {
        var json = await resp.json();
        console.log(json);
        // save to cookies
        setSession("access_token", json.token)
        setSession("userDetails", JSON.stringify(json.userDetails));

        HideAccountShowOther();

        addListeners();
    }
}
async function ULogin(form) {
    // getting form data
    var formData = {
        "username": form["txtUsername2"].value,
        "token": form["txtInviteToken"].value
    }
    var resp = await Send("/api/account/utoken", "POST", formData);
    if (resp.ok) {
        var json = await resp.json();
        console.log(json);
        // save to cookies
        setSession("access_token", json.token)
        setSession("userDetails", JSON.stringify(json.userDetails));

        HideAccountShowOther();

        addListeners();
    }
}
async function Register(form) {
    // getting form data
    var formData = {
        "firstName": form["txtFirstName"].value,
        "lastName": form["txtLastName"].value,
        "email": form["txtEmail"].value,
        "password": form["txtPassword"].value,
        "phoneNumber": form["txtPhoneNumber"].value,
        "companyName": form["txtCompanyName"].value,
        "roleInCompany": form["txtRoleInCompany"].value,
        "description": form["txtDescription"].value,
    };

    var response = await Send("/api/account/register", "POST", JSON.stringify(formData));
    console.log(response);
    if (response.ok) {
        form["txtFirstName"].value = "";
        form["txtLastName"].value = "";
        form["txtEmail"].value = "";
        form["txtPassword"].value = "";
        form["txtPhoneNumber"].value = "";
        form["txtCompanyName"].value = "";
        form["txtRoleInCompany"].value = "";
        form["txtDescription"].value = "";

        alert("Regestration successful! Now you can Log In");
    }
}

function addListeners() {
    document.getElementById("btnJoinToEvent").addEventListener("click", JoinToEvent);
    document.getElementById("btnConnect").addEventListener("click", WebSocketConnect);
    document.getElementById("btnGetOnFloor").addEventListener("click", GetOnFloor);
    document.getElementById("btnTakeASeat").addEventListener("click", TakeASeat);
    document.getElementById("btnGetUp").addEventListener("click", GetUp);
    document.getElementById("btnStartPresent").addEventListener("click", StartPresent);
    document.getElementById("btnEndPresent").addEventListener("click", EndPresent);

    document.getElementById("btnSendMsg").addEventListener("click", SendMessage);

    document.getElementById("btnUserHidden").addEventListener("click", ToggleHide);

    document.getElementById("btnMute").addEventListener("click", ActionMute);
    document.getElementById("btnKick").addEventListener("click", ActionKick);
    document.getElementById("btnJoin").addEventListener("click", ActionJoin);
    document.getElementById("btnBlock").addEventListener("click", ActionBlock);
    document.getElementById("btnUnblock").addEventListener("click", ActionUnblock);

    document.getElementById("btnMakeNoise").addEventListener("click", ActionMakeNoise);

    document.getElementById("btnBuyTicket").addEventListener("click", BuyTicket);
    document.getElementById("btnBuySubscription").addEventListener("click", BuySubscription);
}

// ======   WEB SOCKET   ========

function WebSocketConnect(el, ev) {
    //let eventUserModel = JSON.parse(getSession("eventUserModel"));
    let webSocketUrl = nolimitServerUrl + "/custom?eventId=" + document.getElementById("txtEventId").value;
    document.hubConnection = new signalR.HubConnectionBuilder()
        .withUrl(webSocketUrl, { accessTokenFactory: () => getSession("access_token") })
        .build();

    // listeners
    document.hubConnection.on("UserFloors", UserFloors);
    document.hubConnection.on("UserFloorData", UserFloorData);
    document.hubConnection.on("UserComeIn", UserComeInFloor);
    document.hubConnection.on("UserGoOut", UserGoOut);
    document.hubConnection.on("UserTakeASeat", UserTakeASeat);
    document.hubConnection.on("UserGetUp", UserGetUp);
    document.hubConnection.on("UserStartPresent", UserStartPresent);
    document.hubConnection.on("UserEndPresent", UserEndPresent);
    document.hubConnection.on("UserReceiveMessage", UserReceiveMessage);
    document.hubConnection.on("UserSetSettings", UserSetSettings);

    document.hubConnection.on("EventSpaceBlock", EventSpaceBlock);
    document.hubConnection.on("EventSpaceJoin", EventSpaceJoin);
    document.hubConnection.on("EventSpaceKick", EventSpaceKick);
    document.hubConnection.on("EventSpaceMute", EventSpaceMute);
    document.hubConnection.on("EventSpaceOverview", EventSpaceOverview);
    document.hubConnection.on("EventSpaceNoise", EventSpaceNoise);

    document.hubConnection.on("UserConnected", NewUserConnected);
    document.hubConnection.on("UserDisconnected", UserDisconnected);

    document.hubConnection.start();
}

// SEND MESSAGE

async function JoinToEvent(el, ev) {
    let id = document.getElementById("txtEventId").value;
    let resp = await SendWithAuth("/api/event-space/" + id + "/enter", "POST", {});
    if (resp.ok) {
        var json = await resp.json();
        console.log(json);
        setSession("eventUserModel", JSON.stringify(json));

        LoadSounds();
    }
}
async function GetOnFloor(el, ev) {
    let id = document.getElementById("txtEventId").value;
    let data = {
        id: document.getElementById("txtFloorNum").value
    }
    let resp = await SendWithAuth("/api/event-space/" + id + "/floor/enter", "POST", data);
    //console.log(resp.json());
}
async function TakeASeat(el, ev) {
    let id = document.getElementById("txtEventId").value;
    let data = {
        id: document.getElementById("txtDeskId").value
    }
    var resp = await SendWithAuth("/api/event-space/" + id + "/desk/enter", "POST", data);
    //console.log(resp.json());
}
async function GetUp(el, ev) {
    let id = document.getElementById("txtEventId").value;
    let data = {
        id: document.getElementById("txtDeskId").value
    }
    var resp = await SendWithAuth("/api/event-space/" + id + "/desk/leave", "POST", data);
    //console.log(resp.json());
}
async function StartPresent(el, ev) {
    let id = document.getElementById("txtEventId").value;
    let floorIds = GetSelectedItems("select-floors");
    let speakerIds = GetSelectedItems("select-speakers");
    let data = {
        name: "Presentation",
        floorIds: floorIds,
        speakerIds: speakerIds
    };
    let resp = await SendWithAuth("/api/event-space/" + id + "/presentation/start", "POST", data);
}
async function EndPresent(el, ev) {
    let id = document.getElementById("txtEventId").value;
    let data = {
        id: document.getElementById("txtFloorNum").value
    }
    let resp = await SendWithAuth("/api/event-space/" + id + "/presentation/stop", "POST", data);
}
async function SendMessage(el, ev) {
    let id = document.getElementById("txtEventId").value;
    var data = {
        text: "",
        toDeskId: null,
        toUserId: null
    };
    var selected = document.getElementById("msg-destination").value;
    var destId = document.getElementById("txtDestId").value;
    if (selected) {
        data[selected] = destId;
    }
    data.text = document.getElementById("txtMessageArea").value;
    await SendWithAuth("/api/event-space/" + id + "/chatting", "POST", data);
}
async function ToggleHide(el, ev) {
    let id = document.getElementById("txtEventId").value;
    var data = {
        "isHidden": document.getElementById("user-settings-hidden").value
    };
    await SendWithAuth("/api/event-space/" + id + "/user-settings", "PATCH", data);
}
async function ActionJoin(el, ev) {
    let id = document.getElementById("txtEventId").value;
    let data = {
        id: document.getElementById("txtActToId").value
    }
    console.log(data);
    await SendWithAuth("/api/event-space/" + id + "/join", "POST", data);
}
async function ActionKick(el, ev) {
    let id = document.getElementById("txtEventId").value;
    let data = {
        id: document.getElementById("txtActToId").value
    }
    console.log(data);
    await SendWithAuth("/api/event-space/" + id + "/kick", "POST", data);
}
async function ActionMute(el, ev) {
    let id = document.getElementById("txtEventId").value;
    let data = {
        id: document.getElementById("txtActToId").value
    }
    console.log(data);
    await SendWithAuth("/api/event-space/" + id + "/mute", "POST", data);
}
async function ActionBlock(el, ev) {
    let id = document.getElementById("txtEventId").value;
    let data = {
        id: document.getElementById("txtActToId").value
    }
    //console.log(data);
    await SendWithAuth("/api/event-space/" + id + "/block", "POST", data);
}
async function ActionUnblock(el, ev) {
    let id = document.getElementById("txtEventId").value;
    let data = {
        id: document.getElementById("txtActToId").value
    }
    //console.log(data);
    await SendWithAuth("/api/event-space/" + id + "/unblock", "POST", data);
}
async function ActionMakeNoise(el, ev) {
    let eid = document.getElementById("txtEventId").value;
    let sid = document.getElementById("event-space-sounds").value;
    await SendWithAuth("/api/event-space/" + eid + "/make/noise/" + sid, "POST");
}
async function BuySubscription(el, ev) {
    let stripe = Stripe('pk_test_51I5uG8GURYZu8rjeunEBMhR0vrS2tQZqkBySYJK8K0897syoiDMJBkEN42pCP91nBd8Fev2c8d7mo6TD77lsDpcW001LhcxJNo');
    let planId = 1;
    let response = await SendWithAuth("/api/customer/checkout-session/plan/" + planId, "POST");
    let json = await response.json();
    if (json.sessionId) {
        stripe.redirectToCheckout({ sessionId: json.sessionId })
            .then(function (result) {
                if (result.error) {
                    alert(result.error.message);
                }
            })
            .catch(function (error) {
                console.error('Error:', error);
            });
    } else {
        document.location.href = json.url;
    }
}
async function BuyTicket(el, ev) {
    let eventId = document.getElementById("txtEventId").value;
    let stripe = Stripe('pk_test_51I5uG8GURYZu8rjeunEBMhR0vrS2tQZqkBySYJK8K0897syoiDMJBkEN42pCP91nBd8Fev2c8d7mo6TD77lsDpcW001LhcxJNo');

    let id = document.getElementById("txtEventId").value;
    let response = await SendWithAuth("/api/customer/checkout-session/ticket/" + eventId, "POST");
    let json = await response.json();
    stripe.redirectToCheckout({ sessionId: json.sessionId })
        .then(function (result) {
            if (result.error) {
                alert(result.error.message);
            }
        })
        .catch(function (error) {
            console.error('Error:', error);
        });
}



// GET MESSAGES

function UserComeInFloor(eventUser) {
    console.log('UserComeInFloor ===>');
    console.log(eventUser);
    addChatText(eventUser.userName + " come on " + eventUser.connection.floorId + " floor");
}
function UserGoOut(data) {
    console.log('UserGoOut ===>');
    console.log(data);
    addChatText(data.eventUserId + " go out of the " + data.floorId + " floor");
}
function UserTakeASeat(data) {
    console.log('UserTakeASeat ===>');
    console.log(data);
    addChatText(data.eventUserId + " takes a seat at " + data.deskId + " table");
}
function UserGetUp(data) {
    console.log('UserGetUp ===>');
    console.log(data);
    addChatText(data.eventUserId + " get up from table " + data.deskId);
}
function UserFloorData(floor) {
    console.log('UserFloorData ===>');
    console.log(floor);
    showEventSpaceDesks(floor.desks);
}
function UserFloors(floors) {
    console.log('UserFloors ===>');
    console.log(floors);
    var flhtml = "";
    floors.forEach((v, i) => {
        flhtml = flhtml + "<li>" + v.name + " (" + v.id + ")</li>";
    });
    document.getElementById("ul-floors").innerHTML = flhtml;
}
function UserStartPresent(data) {
    console.log('UserStartPresent ===>');
    console.log(data);
    addChatText("User " + data.eventUserId + " Starts Presentation '" + data.presentation.name + "' on the " + data.floorId + " floor");
    //setSession("presentation", JSON.stringify(data.presentation));
}
function UserEndPresent(data) {
    console.log('UserEndPresent ===>');
    console.log(data);
    addChatText("User " + data.eventUserId + " Ends Presentation on the " + data.floorId + " floor");
}
function UserReceiveMessage(chatMessage) {
    console.log("Chat message ===>");
    console.log(chatMessage);
    addChatText(chatMessage.fromUser.userName + ": " + chatMessage.text);
}
function UserSetSettings(data) {
    console.log("User Set Settings ===>");
    console.log(data);
    addChatText(data.eventUser.userName + "(" + data.eventUser.id + ") is hidden: " + data.eventUser.isHidden);
}


function EventSpaceBlock(blockedUser) {
    console.log("EventSpace Block User ===>");
    console.log(blockedUser);
    addChatText("[Block] User: " + blockedUser.eventUserId);
}
function EventSpaceJoin(joinedUser) {
    console.log("EventSpace Join User ===>");
    console.log(joinedUser);
    addChatText("[Join] User: " + joinedUser.joinedEventUserId + " to the floor: " + joinedUser.toFloorId);
}
function EventSpaceKick(kickedUser) {
    console.log("EventSpace Kick User ===>");
    console.log(kickedUser);
    addChatText("[Kick] User: " + kickedUser.eventUserId);
}
function EventSpaceMute(mutedUser) {
    console.log("EventSpace Mute User ===>");
    console.log(mutedUser);
    addChatText("[Mute] User: " + mutedUser.eventUserId);
}
function EventSpaceOverview(eventSpace) {
    console.log("EventSpace Overview ===>");
    console.log(eventSpace);
    showEventSpaceFloors(eventSpace.floors);
    showEventSpaceUsers(eventSpace.users);
}
function EventSpaceNoise(sid) {
    console.log("EventSpace Noise ===>");
    console.log(sid);
    addChatText("User Made Noise: " + sid);
}

function UserDisconnected(eventuserId) {
    console.log('UserDisconnected ===>');
    console.log(eventuserId);
    addChatText(eventuserId + " disconnected");
}
function NewUserConnected(user) {
    console.log('NewUserConnected ===>');
    addChatText(user.userName + " connected to chat");
}


// LOAD DATA

async function LoadSounds() {
    let soundsSelect = document.getElementById("event-space-sounds");
    let response = await LoadData("/api/sounds");
    if (response.ok) {
        let json = await response.json();
        console.log(json);
        let seloptsHtml = '';
        for (var i = 0; i < json.length; i++) {
            seloptsHtml = seloptsHtml + "<option value='" + json[i].id + "'>" + json[i].title + "</option>";
        }
        soundsSelect.innerHTML = seloptsHtml;
    } else {
        console.log("Status: (" + response.status + ") " + response.statusText);
    }
}


// SHOW DATA

function addChatText(message) {
    let elem = document.createElement("p");
    elem.appendChild(document.createTextNode(message));
    let firstElem = document.getElementById("message-area").firstChild;
    document.getElementById("message-area").insertBefore(elem, firstElem);
}
function showEventSpaceFloors(floors) {
    let flhtml = "";
    let slcFloorsHtml = "";
    floors.forEach((v, i) => {
        flhtml = flhtml + "<li>" + v.name + " (id: " + v.id + ")</li>";
        slcFloorsHtml = slcFloorsHtml + "<option value='" + v.id + "'>" + v.name + " (id: " + v.id + ")</option>";
    });
    document.getElementById("ul-floors").innerHTML = flhtml;
    document.getElementById("select-floors").innerHTML = slcFloorsHtml;
}
function showEventSpaceUsers(users) {
    var userHtml = "";
    let slcUsrHtml = "";
    let speakers = users.filter((v) => v.role === 3);
    users.forEach((v, i) => {
        userHtml = userHtml + "<li>" + v.userName + " (" + v.id + ")</li>";
    });
    speakers.forEach((v, i) => {
        slcUsrHtml = slcUsrHtml + "<option value='" + v.id + "'>" + v.userName + " (id: " + v.id + ", role: " + v.role + ")</option>";
    });
    document.getElementById("ul-users").innerHTML = userHtml;
    document.getElementById("select-speakers").innerHTML = slcUsrHtml;

}
function showEventSpaceDesks(desks) {
    var deskHtml = "";
    desks.forEach((v, i) => {
        deskHtml = deskHtml + "<li>" + v.name + "(" + v.id + ")</li>";
    });
    document.getElementById("ul-desks").innerHTML = deskHtml;
}

function GetSelectedItems(mselectId) {
    let opts = document.getElementById(mselectId).options;
    let selected = Array.apply(null, opts).filter((v, i) => v.selected);
    return selected.map((el) => el.value);
}



async function SignIn(form) {
    // getting form data
    var formData = {
        "username": form["txtUsername"].value
    }
    var resp = await Send("/api/account/utoken", "POST", JSON.stringify(formData));
    if (resp) {
        console.log(resp.userDetails);
        // save to cookies
        setSession("access_token", resp.token)
        setSession("userDetails", JSON.stringify(resp.userDetails));

        HideAccountShowOther();

        CreateSocket();
    }
}







