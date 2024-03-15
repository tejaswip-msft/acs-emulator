const { EmailClient } = require("@azure/communication-email");
require("dotenv").config();

// This code demonstrates how to fetch your connection string
// from an environment variable.
const connectionString = process.env['COMMUNICATION_SERVICES_CONNECTION_STRING'];
const emailClient = new EmailClient(connectionString);

async function main() {
    const POLLER_WAIT_TIME = 10
    try {
      const message = {
        senderAddress: "donotreply@azurecomm.net",
        content: {
          subject: "Welcome to Azure Communication Services Email",
          plainText: "This email message is sent from Azure Communication Services Email using the JavaScript SDK.",
        },
        recipients: {
          to: [
            {
                address: "ageon-d-conqueror@sevenkingdom.com",
                displayName: "Aegon Targaryen",
            },
          ],
        },
        attachments: [
          {
            name: "readme.txt",
            contentType: "text/plain",
            contentInBase64: "ZW1haWwgdGVzdCBhdHRhY2htZW50",
          },
        ],
      };
  
      const poller = await emailClient.beginSend(message);
  
      if (!poller.getOperationState().isStarted) {
        throw "Poller was not started."
      }
  
      let timeElapsed = 0;
      while(!poller.isDone()) {
        poller.poll();
        console.log("Email send polling in progress");
  
        await new Promise(resolve => setTimeout(resolve, POLLER_WAIT_TIME * 1000));
        timeElapsed += 10;
  
        if(timeElapsed > 18 * POLLER_WAIT_TIME) {
          throw "Polling timed out.";
        }
      }
  
      if(poller.getResult().status === "Succeeded") {
        console.log(`Successfully sent the email (operation id: ${poller.getResult().id})`);
      }
      else {
        throw poller.getResult().error;
      }
    } catch (e) {
      console.log(e);
    }
  }
  
  main();