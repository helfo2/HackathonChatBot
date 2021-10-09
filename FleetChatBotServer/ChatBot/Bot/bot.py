import json
import numpy as np
from tensorflow import keras
from sklearn.preprocessing import LabelEncoder
import random
import pickle
from flask import Flask
from flask import jsonify
from flask import request
from logHandler import logger
import loader
import spacy
from difflib import SequenceMatcher

def similar(a, b):
    return SequenceMatcher(None, a, b).ratio()


def minDis(s1, s2, n, m, dp) :
          
  # If any string is empty,
  # return the remaining characters of other string         
  if(n == 0) :
      return m       
  if(m == 0) :
      return n
                    
  # To check if the recursive tree
  # for given n & m has already been executed
  if(dp[n][m] != -1)  :
      return dp[n][m];
                   
  # If characters are equal, execute
  # recursive function for n-1, m-1   
  if(s1[n - 1] == s2[m - 1]) :          
    if(dp[n - 1][m - 1] == -1) :
        dp[n][m] = minDis(s1, s2, n - 1, m - 1, dp)
        return dp[n][m]                  
    else :
        dp[n][m] = dp[n - 1][m - 1]
        return dp[n][m]
         
  # If characters are nt equal, we need to          
  # find the minimum cost out of all 3 operations.        
  else :           
    if(dp[n - 1][m] != -1) :  
      m1 = dp[n - 1][m]     
    else :
      m1 = minDis(s1, s2, n - 1, m, dp)
              
    if(dp[n][m - 1] != -1) :               
      m2 = dp[n][m - 1]           
    else :
      m2 = minDis(s1, s2, n, m - 1, dp)  
    if(dp[n - 1][m - 1] != -1) :   
      m3 = dp[n - 1][m - 1]   
    else :
      m3 = minDis(s1, s2, n - 1, m - 1, dp)
     
    dp[n][m] = 1 + min(m1, min(m2, m3))
    return dp[n][m]


if __name__ == '__main__':
    """ Runs the Chat Bot """
        
    data, model, tokenizer, lbl_encoder, entities = loader.loadBot()
    nlp = spacy.load('en_core_web_sm') # needs to be downloaded separately

    entity_names = []
    for entity in entities['Equipment']:
        entity_names.append(entity['Name'])
    
    # parameters
    max_len = 20

    chatMode = False

    if chatMode:
        print("Start messaging with the bot (type quit to stop)!")

        while True:
            inp = input()
            if inp.lower() == "quit":
                break

            result = model.predict(keras.preprocessing.sequence.pad_sequences(tokenizer.texts_to_sequences([inp]),
                                                    truncating='post', maxlen=max_len))

            if(np.argmax(result) < 0.5):
                print('Sorry, I didn\'t understand')
            else:
                tag = lbl_encoder.inverse_transform([np.argmax(result)])

                #logger.debug('result: ', result.__str__())
                #logger.debug('tag: ', tag.__str__())

                for i in data['intents']:
                    if i['tag'] == tag:
                        print("ChatBot: ", np.random.choice(i['responses']))
    else:
        app = Flask(__name__)

        @app.route('/api/chatbot/v1/inference', methods=['POST'])
        def infer():
            request_data = json.loads(request.data)
            
            phrase = request_data['phrase']

            distances = []
            phrase_entity = ''
            for entity_name in entity_names:
                # Driver code
                #str1 = "voldemort"
                #str2 = "dumbledore"
    
                #n = len(entity_name)
                #m = len(phrase)
                #dp = [[-1 for i in range(m + 1)] for j in range(n + 1)]
                
                ##print(minDis(str1, str2, n, m, dp))
                #distances.append(minDis(entity_name, phrase, n, m, dp))

                if phrase.lower().find(entity_name.lower()) is not -1:
                    phrase_entity = entity_name

                #distances.append(similar(entity_name, phrase))

            #min_dist = 1e9
            #entity_index = 0
            #for i, edit_distance in enumerate(distances):
            #    if edit_distance < min_dist:
            #        min_dist = edit_distance
            #        entity_index = i
            
            #for i, distance in enumerate(distances):
            #    print(entity_names[i])
            #    print(distances[i])

            #entity = entity_names[entity_index]

            print('Closest entity: ', phrase_entity)

            #doc = nlp(phrase)

            #sub_toks = [str(tok) for tok in doc if (tok.dep_ == 'compound' or tok.dep_ == 'pobj' or tok.dep_ == 'nummod')]
            #entity = ' '.join(sub_toks)
            entity = phrase_entity

            result = model.predict(keras.preprocessing.sequence.pad_sequences(tokenizer.texts_to_sequences([phrase]),
                                                    truncating='post', maxlen=max_len))

            if np.argmax(result) < 0.5:
                return jsonify(
                    {
                        'tag': 'undefined',
                        'answer': 'Sorry, I didn\'t understand.\nTry asking what is the current state of [Equipment Name]',
                        'uri': '',
                        'predicate': ''
                    })
            else:
                tag = lbl_encoder.inverse_transform([np.argmax(result)])

                #logger.debug('result: ', result.__str__())
                #logger.debug('tag: ', tag.__str__())

                if entity == '':
                    if str(tag[0]) == 'equipment_current_state' or str(tag[0]) == 'equipment_current_operator' or str(tag[0]) == 'equipment_current_state_time':
                        return jsonify(
                            {
                                'tag': 'undefined',
                                'answer': 'Sorry, I didn\'t understand.\nTry asking what is the current state of [Equipment Name]',
                                'uri': '',
                                'predicate': ''
                            })

                for i in data['intents']:
                    if i['tag'] == tag:
                        url = ''
                        if 'url' in i:
                            url = i['url']
                        
                        return jsonify(
                            {
                                'tag': i['tag'],
                                'answer': np.random.choice(i['responses']),
                                'uri': url,
                                'predicate': entity
                            })

            return jsonify(
                    {
                        'tag': 'undefined',
                        'answer': 'Sorry, I didn\'t understand.\nTry asking what is the current state of [Equipment Name]',
                        'uri': '',
                        'predicate': ''
                    })

        
        app.run(host='localhost', port=12345)
