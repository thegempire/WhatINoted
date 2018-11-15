// Initialize Firebase
var config = {
    apiKey: "AIzaSyBBYx2A-6F1IMdWhFBEudrPZjPiWJU-Y60",
    authDomain: "whatinoted-12345.firebaseapp.com",
    databaseURL: "https://whatinoted-12345.firebaseio.com",
    projectId: "whatinoted-12345",
    storageBucket: "whatinoted-12345.appspot.com",
    messagingSenderId: "669145632626"
};
firebase.initializeApp(config);

// FirebaseUI config.
var uiConfig = {
    signInSuccessURL: './Notebooks.aspx',
    signInOptions: [
        firebase.auth.GoogleAuthProvider.PROVIDER_ID,
        firebase.auth.FacebookAuthProvider.PROVIDER_ID,
        firebase.auth.TwitterAuthProvider.PROVIDER_ID
    ]
};

function handleLoginForLoginPage() {
    firebase.auth().onAuthStateChanged(function (user) {
        if (user) {
            // User is logged in
            window.location = "./Notebooks.aspx";
        } else {
            // User is logged out
        }
    });
}

function handleLoginForContentPage() {
    firebase.auth().onAuthStateChanged(function (user) {
        if (user) {
            // User is logged in
            document.getElementsByClassName("navbar-logout")[0].classList.remove("hidden");
            //var data = '{userID: "' + user.uid + '", displayName: "' + user.displayName + '", email: "' + user.email + '" }'; // TODO - remove
            var data = JSON.stringify({userID: user.uid, displayName: user.displayName, email: user.email });
            callCSMethod("Notebooks.aspx/HandleLogin", data, function () {
                let userIDElement = document.getElementById("MainContent_HandleLoginUserID");
                userIDElement.value = user.uid;
                let triggerButton = document.getElementsByClassName('handleLoginTrigger')[0];
                triggerButton.click();
            }.bind(user))
        } else {
            // User is logged out
            window.location = "./Login.aspx";
        }
    });
}

function logout() {
    firebase.auth().signOut().then(function () {
        window.location = "./";
    });
}