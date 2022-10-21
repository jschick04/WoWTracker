# Introduction
WoW Tracker is an application for coordinating crafting between multiple characters.
Each character can track items that they need and items that they are able to craft that other characters need.

Eventually will add a feature to track how many materials are needed to craft all required items.
This information will sync with a character marked as a bank alt who will see the entire required items list.

# Getting Started
1. Docker SQL Deployment
Create an .env file containing the following information
SA=Password
DATAPATH=Path_To_Volume

Once this is set run "docker-compose up -d" from root directory