## BuddyTask (under development)

This project is a web application for students to help them stay organized and manage their tasks.

### Features

- [x] : Task management with the ability to add, modify, and delete items.
- [x] : Organizing tasks by categories (studies, projects, exams, etc.).
- [ ] : Task scheduling with due dates.
- [ ] : Notifications for upcoming tasks.
- [ ] : Calendar view for better visualization of tasks and deadlines.

### Prerequisites

To run this application locally, make sure you have installed [Docker Desktop](https://docs.docker.com/desktop/install/windows-install/)

### Usage

Clone this repository on your machine, then execute using Docker Compose : `docker-compose up`

- Access the frontend application in your browser at http://localhost:5173.
- You can visualize route on Swagger using this url : https://localhost:7174/swagger/index.html

To stop the containers, run the following command in your terminal : `docker-compose down`

### Technologies used

Backend: ASP.NET 6 Web API  
Frontend: Vue.js 3  
Database: MongoDB
