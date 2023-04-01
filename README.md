## BuddyTask - Stay Organized and On Top of Your Work!

This project is a web application for students to help them stay organized and manage their tasks.

### Features

- [x] : Task management with the ability to add, modify, and delete items.
- [] : Organizing tasks by categories (studies, projects, exams, etc.).
- [] : Task scheduling with due dates.
- [] : Notifications for upcoming tasks.
- [] : Calendar view for better visualization of tasks and deadlines.

### Prerequisites

To run this application locally, make sure you have installed [Docker Desktop](https://docs.docker.com/desktop/install/windows-install/)

### Usage

Clone this repository on your machine, then execute using Docker Compose

`docker-compose up`

Access the frontend application in your browser at http://localhost:5173.
To access the backend API, use the URL http://localhost:7174.
You can visualize route on Swagger using this url : http://localhost:7174/swagger/index.html

To stop the containers and remove the resources created by Docker Compose, run the following command in your terminal:

`docker-compose down`

### Technologies used

Backend: ASP.NET 6 Web API
Frontend: Vue.js 3
Database: MongoDB
