import redis
import os

redisHostname = os.environ.get('REDIS_HOSTNAME')
accesskey = os.environ.get('REDIS_KEY')

r = redis.StrictRedis(host=redisHostname,port=6380,password=accesskey,ssl=True)
result = r.ping()
print("Ping returned : " + str(result))

#Create Key Values
result = r.set("Animal", "Cat")
print("Created key: Animal")
result =  r.set("Insect", "Grasshopper")
print("Created key: Insect")
result = r.set("Pokemon", "Squirtle")
print("Created key: Pokemon")

#Retrieve Value from Key
result = r.get('Animal')
print("Retrieved Value from Animal Key: " + result.decode("utf-8"))
result = r.get('Insect')
print("Retrieved Value from Insect Key: " + result.decode("utf-8"))
result = r.get('Pokemon')
print("Retrieved Value from Pokemon Key: " + result.decode("utf-8"))