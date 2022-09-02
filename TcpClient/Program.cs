using ClientTcp;

String server = "127.0.0.1";
Int32 port = 8888;

string message = "Привет мир!, Hello world";

Client client = new Client(server, port);
client.SendMessage(message);