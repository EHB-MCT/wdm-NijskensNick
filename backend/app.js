const express = require("express");
const router = express.Router();
const {MongoClient, ObjectId} = require('mongodb');
const http = require('http');
const bodyParser = require("body-parser");
const cors = require('cors');
const app = express();
const path = require('path');

// Global variables
const DATABASE_USERNAME = process.env.MONGO_INITDB_ROOT_USERNAME;
const DATABASE_PASSWORD = process.env.MONGO_INITDB_ROOT_PASSWORD;
const DATABASE_DB = process.env.MONGO_INITDB_DATABASE;
const DATABASE_HOST = process.env.DATABASE_HOST;
const DATABASE_PORT = process.env.DATABASE_PORT;

const SERVER_HOST = process.env.SERVER_HOST;
const SERVER_PORT = process.env.SERVER_PORT;


// Connect with database
const URI = `mongodb://${DATABASE_USERNAME}:${DATABASE_PASSWORD}@${DATABASE_HOST}:${DATABASE_PORT}`;
const client = new MongoClient(URI);
const db = client.db(DATABASE_DB);

// Middleware
app.use(bodyParser.urlencoded({extended: true}));
app.use(bodyParser.json());
app.use(cors());

// Listening message
app.listen(SERVER_PORT, (err) => {
    if(!err)
    {
        console.log("running on port " + SERVER_PORT);
    }
    else
    {
        console.error(err);
    }
})