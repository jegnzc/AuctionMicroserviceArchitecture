package com.onlineauctionweb.bidservice;

import com.onlineauctionweb.models.*;
import com.microsoft.applicationinsights.TelemetryClient;
import org.apache.kafka.clients.producer.Callback;
import org.apache.kafka.clients.producer.KafkaProducer;
import org.apache.kafka.clients.producer.ProducerConfig;
import org.apache.kafka.clients.producer.ProducerRecord;
import org.apache.kafka.clients.producer.RecordMetadata;
import org.apache.kafka.common.serialization.LongSerializer;
import org.apache.kafka.common.serialization.StringSerializer;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.ResponseBody;
import java.net.UnknownHostException;
import java.text.SimpleDateFormat;
import java.time.LocalDateTime;
import java.util.*;
import org.springframework.beans.factory.annotation.Autowired;
import com.microsoft.applicationinsights.telemetry.Duration;
import com.google.gson.Gson;
import com.mongodb.*;

@Controller
public class BidController {

    @Autowired
    TelemetryClient telemetryClient;

    @CrossOrigin(origins = "*", allowedHeaders = "*")
    @RequestMapping(value = "/bid", method = RequestMethod.POST)
    @ResponseBody
    public void CreateBid(String bidAmount, String userID, String auctionID, String userName) throws Exception {
        try {
            telemetryClient.trackTrace("Creating bid");

            MongoClient mongoClient = new MongoClient(new MongoClientURI(
                    "mongodb://mongodboas:MQrA7uPaXBVXG9aVi6W8blApdQFIzLgGjCdjIKOF6BKopaa4pOhb8R2izvW2VxwXlUxnidgxnrVmDFyIRt8R2w==@mongodboas.mongo.cosmos.azure.com:10255/?ssl=true&retrywrites=false&replicaSet=globaldb&maxIdleTimeMS=120000&appName=@mongodboas@"));

            DB db = mongoClient.getDB("biddb");
            DBCollection coll = db.getCollection("bids");
            SimpleDateFormat dateFormat = new SimpleDateFormat("yyyy/MM/dd HH:mm:ss");
            LocalDateTime now = LocalDateTime.now();

            BasicDBObject doc = new BasicDBObject();
            doc.append("bidid", UUID.randomUUID().toString());
            doc.append("auctionId", auctionID);
            doc.append("bidAmount", bidAmount);
            doc.append("userId", userID);
            doc.append("userName", userName);
            doc.append("bidDate", System.currentTimeMillis());

            try {
                /*
                 * Defining producer properties.
                 */
                Properties properties = new Properties();
                telemetryClient.trackTrace("Sending messages to Kafka bid topic");
                // properties.load(new FileReader("src/main/resources/producer.config"));
                properties.put("bootstrap.servers", "onlineauctionkafkahub.servicebus.windows.net:9093");
                properties.put("security.protocol", "SASL_SSL");
                properties.put("sasl.mechanism", "PLAIN");
                properties.put("sasl.jaas.config",
                        "org.apache.kafka.common.security.plain.PlainLoginModule required username=\"$ConnectionString\" password=\"enter connection string\";");
                properties.put(ProducerConfig.KEY_SERIALIZER_CLASS_CONFIG, LongSerializer.class.getName());
                properties.put(ProducerConfig.VALUE_SERIALIZER_CLASS_CONFIG, StringSerializer.class.getName());

                KafkaProducer<Long, String> producer = new KafkaProducer<>(properties);
                long time = System.currentTimeMillis();
                Gson gson = new Gson();
                String bidObjectJson = gson.toJson(doc);
                final ProducerRecord<Long, String> record = new ProducerRecord<Long, String>("bidtopic", time,
                        bidObjectJson);
                producer.send(record, new Callback() {
                    public void onCompletion(RecordMetadata metadata, Exception exception) {
                        if (exception != null) {
                            System.out.println(exception);
                            System.exit(1);
                        }
                    }
                });

                telemetryClient.trackTrace("Message sent to Kafka bid topic");
            } catch (Exception ex) {
                System.out.print(ex.getMessage());
                throw ex;
            }

            coll.insert(doc);

        } catch (UnknownHostException e) {
            e.printStackTrace();
        }
    }

    @CrossOrigin(origins = "*", allowedHeaders = "*")
    @RequestMapping(value = "/bid", method = RequestMethod.GET)
    @ResponseBody
    public ArrayList GetBidByAuctionID(String auctionID) {
        ArrayList lst = new ArrayList();

        telemetryClient.trackTrace("Getting Bid by Auction ID");

        try {
            MongoClient mongoClient = new MongoClient(new MongoClientURI(
                    "mongodb://mongodboas:MQrA7uPaXBVXG9aVi6W8blApdQFIzLgGjCdjIKOF6BKopaa4pOhb8R2izvW2VxwXlUxnidgxnrVmDFyIRt8R2w==@mongodboas.mongo.cosmos.azure.com:10255/?ssl=true&retrywrites=false&replicaSet=globaldb&maxIdleTimeMS=120000&appName=@mongodboas@"));

            DB db = mongoClient.getDB("biddb");
            DBCollection coll = db.getCollection("bids");
            BasicDBObject doc = new BasicDBObject();
            doc.put("auctionId", auctionID);
            DBCursor cursor = coll.find(doc);

            Integer counter = 1;

            while (cursor.hasNext()) {
                BasicDBObject obj = (BasicDBObject) cursor.next();
                BidDetail bidDetail = new BidDetail();
                bidDetail.bidID = counter.toString();
                bidDetail.amount = obj.getString("bidAmount");
                bidDetail.customer = obj.getString("userId");
                bidDetail.customerName = obj.getString("userName");
                bidDetail.bidAt = obj.getString("bidDate");
                lst.add(bidDetail);
                counter++;
            }

        } catch (UnknownHostException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        }

        return lst;
    }

}