
class WebCLI {
    
    /**
    * Constructor for the WebCLI object. 
    * @class WebCLI
    * @memberof WebCLI
    * @param {HostSettings} object representing the host's particular settings. 
    * @example HostSettings = { commandUrl:"http:\\url\data", queryUrl:"http:\\url\data" }
    */
    constructor(hostSettings) {

        var self = this;

        self.hostSettings = hostSettings;

        self.history = []; // Command History

        self.cmdOffset = 0;

        self.createElements();

        self.wireEvents();

        self.showGreeting();

        self.focus();
    }

    wireEvents() {

        var self = this;

        self.keyDownHandler = function (e) { self.onKeyDown(e); }
        self.clickHandler = function (e) { self.onClick(e); }

        document.addEventListener("keydown", self.keyDownHandler);
        self.ctrlEl.addEventListener("click", self.clickHandler)
    }

    onKeyDown(e) {

        var self = this, ctrlStyle = self.ctrlEl.style;

        // Open/Close the Console
        if (e.ctrlKey && e.keyCode === 192) {
            if (ctrlStyle.display === "none") {
                ctrlStyle.display = ""
                self.focus();
            }
            else {
                ctrlStyle.display = "none";

            }
        }

        // Key Handlers inside the console //
        if (self.inputEl === document.activeElement) {

            switch (e.keyCode) {

                case 13: // Enter
                    return self.runCmd();
                case 38: // up
                    // If there is history then push it to the window //
                    if ((self.history.length + self.cmdOffset) > 0) {
                        self.cmdOffset--;
                        self.inputEl.value = self.history[self.history.length + self.cmdOffset];
                        e.preventDefault();
                    }
                    break;
                case 40: // down
                    // If there is history then push it to the window //
                    if (self.cmdOffset < -1) {
                        self.cmdOffset++;
                        self.inputEl.value = self.history[self.history.length + self.cmdOffset];
                        e.preventDefault();
                    }
                    break;
            }
        }
    }

    runCmd() {
        var self = this, txt = self.inputEl.value.trim();

        self.cmdOffset = 0;         // RESET the history back to 0
        self.inputEl.value = "";    // Clear input
        self.writeLine(txt, "cmd"); // Write cmd to the output
        if (txt === "") { return; } // if nothing is entered return 
        self.history.push(txt);     // Add the cmd to the history

        // Client Command //
        // Should be in one of the following formats to allow actions to occur //
        // COMMAND) -c clear caches - this is a command example, it will alter the state on the server
        // QUERY) -q select * from instns - this will execute a query on the server, does not change the state
        var tokens = txt.split(" "),
            taskType = tokens[0].toUpperCase(),
            data = "";

        if (tokens.length > 1)
            data = tokens[1];

        if (taskType === "CLS") { self.outputEl.innerHTML = ""; return; }

        if (task === "-c") {
            // Command action //
            fetch(self.hostSettings.commandUrl,
            {
                method: "post",
                headers: new Headers({ "Content-Type": "application/json" }),
                body: JSON.stringify({
                    commandData: {
                        "task": "command",
                        "data": data
                    }
                })
            })
            .then(function (r) { return r.json(); })
            .then(function (result) {

                var output = result.output;
                var style = result.isError ? "error" : "ok";

                if (result.isHTML) {
                    self.writeHTML(output);
                }
                else {
                    self.writeLine(output);
                }
            })
            .catch(function (err) {
                self.writeLine("Error sending command to server : " + err, "error");
            });
        }
        else if (taskType === "-q") {
            // Query action //
            fetch(self.hostSettings.queryUrl + "?task=query&data=" + data,
            {
                method: "get",
                headers: new Headers({ "Content-Type": "application/json" }),
            })
            .then(function (result) {

                var output = result.output;
                var style = result.isError ? "error" : "ok";

                if (result.isHTML) {
                    self.writeHTML(output);
                }
                else {
                    self.writeLine(output);
                }
            })
            .catch(function (err) {
                self.writeLine("Error sending command to server : " + err, "error");
            });
        }
    }

    onClick(e) {
        this.focus();
    }

    focus() {
        this.inputEl.focus();
    }

    scrollToBottom() {
        this.ctrlEl.scrollTop = this.ctrlEl.scrollHeight;
    }

    newLine() {
        this.outputEl.appendChild(document.createElement("br"));
        this.scrollToBottom();
    }

    writeLine(txt, cssSuffix) {
        var span = document.createElement("span");
        cssSuffix = cssSuffix || "ok";
        span.className = "webcli-" + cssSuffix;
        span.innerText = txt;
        this.outputEl.appendChild(span);
        this.newLine();
    }

    writeHTML(markup) {
        var div = document.createElement("div");
        div.innerHTML = markup;
        this.outputEl.appendChild(div);
        this.newLine();
    }

    showGreeting() {
        this.writeLine("WebCLI [Version 1.0]", "cmd");
        this.newLine();
    }

    createElements() {
        var self = this,
            doc = document;

        // CLI Elements //
        self.ctrlEl = doc.createElement("div");
        self.outputEl = doc.createElement("div");
        self.inputEl = doc.createElement("input");

        self.ctrlEl.className = "webcli";
        self.outputEl.className = "webcli-output";
        self.inputEl.className = "webcli-input";

        self.inputEl.setAttribute("spellcheck", "false");

        self.ctrlEl.appendChild(self.outputEl);
        self.ctrlEl.appendChild(self.inputEl);

        doc.body.appendChild(self.ctrlEl);
    }
}