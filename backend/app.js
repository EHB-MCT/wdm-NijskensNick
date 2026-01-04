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

const DATABASE_STANDINGSTILL_COLLECTION = process.env.DATABASE_STANDINGSTILL_COLLECTION;
const DATABASE_PASSINGTHROUGH_COLLECTION = process.env.DATABASE_PASSINGTHROUGH_COLLECTION;
const DATABASE_SESSIONS_COLLECTION = process.env.DATABASE_SESSIONS_COLLECTION;
const DATABASE_DEVICENAME_COLLECTION = process.env.DATABASE_DEVICENAME_COLLECTION;
const DATABASE_USERNAME_COLLECTION = process.env.DATABASE_USERNAME_COLLECTION;


// Connect with database
const URI = `mongodb://${DATABASE_USERNAME}:${DATABASE_PASSWORD}@${DATABASE_HOST}:${DATABASE_PORT}`;
const client = new MongoClient(URI);
const db = client.db(DATABASE_DB);

// Collections
const collection_ss = db.collection(DATABASE_STANDINGSTILL_COLLECTION);
const collection_pt = db.collection(DATABASE_PASSINGTHROUGH_COLLECTION);
const collection_sessions = db.collection(DATABASE_SESSIONS_COLLECTION);
const collection_devicenames = db.collection(DATABASE_DEVICENAME_COLLECTION);
const collection_usernames = db.collection(DATABASE_USERNAME_COLLECTION);

// Middleware
app.use(bodyParser.urlencoded({extended: true}));
app.use(bodyParser.json());
app.use(cors());

// Standing Still Routes
app.route('/StandingStillPairs')
    .get(async (req, res) => {
        try {
            if(req.query.devicename != undefined)
            {
                await client.connect();
                const query = {"deviceName": String(req.query.devicename)};
                const result = await collection_ss.find(query).toArray();
                res.send(JSON.stringify(result));
                return;
            }
            if(req.query.username != undefined)
            {
                await client.connect();
                const query = {"userName": String(req.query.username)};
                const result = await collection_ss.find(query).toArray();
                res.send(JSON.stringify(result));
            }
            await client.connect();
            const result = await collection_ss.find({}).toArray();
            res.send(JSON.stringify(result));
        } catch (err) {
            console.error(err)
        } finally {
            await client.close();
        }
    })
    .post(async (req, res) => {
        try {
            await client.connect();
            if(!await collection_devicenames.findOne({"deviceName": String(req.body.deviceName)}))
            {
                collection_devicenames.insertOne({"deviceName": String(req.body.deviceName)})
                .then((result) => {
                    console.log("Added device " + String(result.insertedId));
                })
            }
            if(!await collection_usernames.findOneAndDelete({"deviceName": String(req.body.deviceName), "userName": String(req.body.userName)}))
            {
                collection_usernames.insertOne({"deviceName": String(req.body.deviceName), "userName": String(req.body.userName)})
                .then((result) => {
                    console.log("Added user " + String(result.insertedId) + " to device " + String(req.body.deviceName));
                })
            }
            const testQuery = {deviceName: String(req.body.deviceName), userName: String(req.body.userName), startTime: Date(req.body.startTime)}
            const test = await collection_ss.findOne(testQuery);
            if(test) {
                res.status(400).send('Bad request: pair already exists');
                return;
            }
            var newStandingStillPair = {
                deviceName: req.body.deviceName,
                userName: req.body.userName,
                startTime: req.body.startTime,
                endTime: req.body.endTime,
                ended: req.body.ended
            }
            await collection_ss.insertOne(newStandingStillPair)
            .then(result => {
                console.log(result.insertedId);
                res.send(String(result.insertedId));
            });
            return;
        } catch (err) {
            console.error(err)
        } finally {
            await client.close();
        }
    })

app.route('/StandingStillPairs/:id')
    .get(async (req, res) => {
        try {
            await client.connect();
            var id = new ObjectId(String(req.params.id));
            const query = {"_id": id};
            const result = await collection_ss.findOne(query);
            res.send(JSON.stringify(result));
        } catch (err) {
            console.error(err);
        } finally {
            client.close();
        }
    })
    .put(async (req, res) => {
        try {
            await client.connect();
            var id = new ObjectId(String(req.params.id));
            const query = {"_id": id};
            var updates = { $set: req.body };
            console.log(updates);
            await collection_ss.updateOne(query, updates);
            res.send(id);
        } catch (err) {
            console.error(err);
        } finally {
            client.close();
        }
    })
    .delete(async (req, res) => {
        try {
            await client.connect();
            var id = new ObjectId(String(req.params.id));
            const query = {"_id": id};
            await collection_ss.deleteOne(query);
            res.status(200).json({
                message: "SSPair deleted"
            });
        } catch (err) {
            console.error(err);
        } finally {
            client.close();
        }
    })

// Passing Through Routes
app.route('/PassingThroughPairs')
    .get(async (req, res) => {
        try {
            if(req.query.devicename != undefined)
            {
                await client.connect();
                const query = {"deviceName": String(req.query.devicename)};
                const result = await collection_pt.find(query).toArray();
                res.send(JSON.stringify(result));
                return;
            }
            if(req.query.username != undefined)
            {
                await client.connect();
                const query = {"userName": String(req.query.username)};
                const result = await collection_pt.find(query).toArray();
                res.send(JSON.stringify(result));
                return;
            }
            await client.connect();
            res.send(JSON.stringify(await collection_pt.find({}).toArray()))
        } catch (err) {
            console.error(err);
        } finally {
            client.close();
        }
    })
    .post(async (req, res) => {
        try {
            await client.connect();
            if(!await collection_devicenames.findOne({"deviceName": String(req.body.deviceName)}))
            {
                collection_devicenames.insertOne({"deviceName": String(req.body.deviceName)})
                .then((result) => {
                    console.log("Added device " + String(result.insertedId));
                })
            }
            if(!await collection_usernames.findOne({"deviceName": String(req.body.deviceName), "userName": String(req.body.userName)}))
            {
                collection_usernames.insertOne({"deviceName": String(req.body.deviceName), "userName": String(req.body.userName)})
                .then((result) => {
                    console.log("Added user " + String(result.insertedId) + " to device " + String(req.body.deviceName));
                })
            }
            const testQuery = {deviceName: String(req.body.deviceName), userName: String(req.body.userName), startTime: Date(req.body.startTime)}
            const test = await collection_pt.findOne(testQuery);
            if(test) {
                res.status(400).send('Bad request: pair already exists');
                return;
            }
            var newStandingStillPair = {
                deviceName: req.body.deviceName,
                userName: req.body.userName,
                startTime: req.body.startTime,
                endTime: req.body.endTime,
                ended: req.body.ended
            }
            await collection_pt.insertOne(newStandingStillPair)
            .then(result => {
                console.log(String(result.insertedId));
                res.send(String(result.insertedId));
            });
        } catch (err) {
            console.error(err);
        } finally {
            client.close();
        }
    })

app.route('/PassingThroughPairs/:id')
    .get(async (req, res) => {
        try {
            await client.connect();
            var id = new ObjectId(String(req.params.id));
            const query = {"_id": id};
            const result = await collection_pt.findOne(query);
            res.send(JSON.stringify(result));
        } catch (err) {
            console.error(err);
        } finally {
            client.close();
        }
    })
    .put(async (req, res) => {
        try {
            await client.connect();
            var id = new ObjectId(String(req.params.id));
            const query = {"_id": id};
            var updates = { $set: req.body }
            let updateResult = await collection_pt.updateOne(query, updates);
            console.log(updateResult._id);
        } catch (err) {
            console.error(err);
        } finally {
            client.close();
        }
    })
    .delete(async (req, res) => {
        try {
            await client.connect();
            var id = new ObjectId(String(req.params.id));
            const query = {"_id": id};
            await collection_pt.deleteOne(query);
            res.status(200).json({
                message: "PTPair deleted"
            });
        } catch (err) {
            console.error(err);
        } finally {
            client.close();
        }
    })

// Sessions Routes
app.route('/Sessions')
    .get(async (req, res) => {
        try {
            if(req.query.devicename != undefined)
            {
                await client.connect();
                const query = {"deviceName": String(req.query.devicename)};
                const result = await collection_sessions.find(query).toArray();
                res.send(JSON.stringify(result));
                return;
            }
            if(req.query.username != undefined)
            {
                await client.connect();
                const query = {"userName": String(req.query.username)};
                const result = await collection_sessions.find(query).toArray();
                res.send(JSON.stringify(result));
                return;
            }
            await client.connect();
            const result = await collection_sessions.find({}).toArray();
            res.send(JSON.stringify(result));
        } catch (err) {
            console.error(err);
        } finally {
            client.close();
        }
    })
    .post(async (req, res) => {
        try {
            await client.connect();
            if(!await collection_devicenames.findOne({"deviceName": String(req.body.deviceName)}))
            {
                collection_devicenames.insertOne({"deviceName": String(req.body.deviceName)})
                .then((result) => {
                    console.log("Added device " + String(result.insertedId));
                })
            }
            if(!await collection_usernames.findOne({"deviceName": String(req.body.deviceName), "userName": String(req.body.userName)}))
            {
                collection_usernames.insertOne({"deviceName": String(req.body.deviceName), "userName": String(req.body.userName)})
                .then((result) => {
                    console.log("Added user " + String(result.insertedId) + " to device " + String(req.body.deviceName));
                })
            }
            const testQuery = {deviceName: String(req.body.deviceName), userName: String(req.body.userName), startTime: Date(req.body.startTime)}
            const test = await collection_sessions.findOne(testQuery);
            if(test) {
                res.status(400).send('Bad request: session already exists');
                return;
            }
            var newSession = {
                deviceName: req.body.deviceName,
                userName: req.body.userName,
                startTime: req.body.startTime,
                endTime: req.body.endTime,
                ended: req.body.ended
            }
            await collection_sessions.insertOne(newSession)
            .then((result) => {
                console.log(String(result.insertedId));
                res.send(String(result.insertedId));
            })
        } catch (err) {
            console.error(err);
        } finally {
            client.close();
        }
    })

app.route('/Sessions/:id')
    .get(async (req, res) => {
        try {
            await client.connect();
            var id = new ObjectId(String(req.params.id));
            var query = {"_id": id};
            const result = await collection_sessions.findOne(query);
            res.send(JSON.stringify(result));
        } catch (err) {
            console.error(err);
        } finally {
            client.close();
        }
    })
    .put(async (req, res) => {
        try {
            await client.connect();
            var id = new ObjectId(String(req.params.id));
            const query = {"_id": id};
            var updates = { $set: req.body };
            let updateResult = await collection_sessions.updateOne(query, updates);
            console.log(updateResult._id);
        } catch (err) {
            console.error(err);
        } finally {
            client.close();
        }
    })
    .delete(async (req, res) => {
        try {
            await client.connect();
            var id = new ObjectId(String(req.params.id));
            const query = {"_id": id};
            await collection_sessions.deleteOne(query);
            res.status(200).json({
                message: "Session deleted"
            });
        } catch (err) {
            console.error(err);
        } finally {
            client.close();
        }
    })

// Devicename Routes
app.route('/LoggedDeviceNames')
    .get(async (req, res) => {
        try {
            await client.connect();
            const result = await collection_devicenames.find({}).toArray();
            res.send(JSON.stringify(result));
        } catch (err) {
            console.error(err);
        } finally {
            client.close();
        }
    })
    .post(async (req, res) => {
        if(await collection_devicenames.findOne({deviceName: String(req.body.deviceName)}))
        {
            res.status(400).send('Bad request: device already exists');
            return;
        }
        await collection_devicenames.insertOne({deviceName: String(req.body.deviceName)})
        .then((result) => {
            console.log("Added device " + String(result.insertedId));
        })
    })

app.route('/LoggedDeviceNames/:id')
    .delete(async (req, res) => {
        try {
            await client.connect();
            var id = new ObjectId(String(req.params.id));
            const query = {"_id": id};
            await collection_devicenames.deleteOne(query);
            res.status(200).json({
                message: "Device name deleted"
            });
        } catch (err) {
            console.error(err);
        } finally {
            client.close();
        }
    })

// Username Routes
app.route('/LoggedUserNames')
    .get(async (req, res) => {
        try {
            if(req.query.devicename != undefined)
            {
                await client.connect();
                res.send(JSON.stringify(await collection_usernames.find({deviceName: String(req.query.devicename)}).toArray()))
                return;
            }
            await client.connect();
            const result = await collection_usernames.find({}).toArray();
            res.send(JSON.stringify(result));
        } catch (err) {
            console.error(err);
        } finally {
            client.close();
        }
    })
    .post(async (req, res) => {
        if(await collection_usernames.findOne({deviceName: String(req.body.deviceName), userName: String(req.body.userName)}))
        {
            res.status(400).send('Bad request: username already exists for device ' + String(req.body.deviceName));
            return;
        }
        await collection_usernames.insertOne({deviceName: String(req.body.deviceName), userName: String(req.body.userName)})
        .then((result) => {
            console.log("Added user " + String(result.insertedId) + " to device " + String(req.body.deviceName));
        })
    })

app.route('/LoggedUserNames/:id')
    .delete(async (req, res) => {
        try {
            await client.connect();
            var id = new ObjectId(String(req.params.id));
            const query = {"_id": id};
            await collection_usernames.deleteOne(query);
            res.status(200).json({
                message: "Username deleted"
            });
        } catch (err) {
            console.error(err);
        } finally {
            client.close();
        }
    })

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