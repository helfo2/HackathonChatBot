from logHandler import logger
from tensorflow import keras
import json 
import traceback
import pickle
import os 
dir_path = os.path.dirname(os.path.realpath(__file__))
dir_path += '\ChatModel'

def loadEntities():
    """ Loads the entities of our domain (UG Pro) """
    dir = os.path.dirname(os.path.dirname(__file__))
    dir = os.path.dirname(dir) + '\ChatBot\Configuration'
    with open(dir + "\ChatBotPredicates.json", encoding='utf-8-sig') as file:
        data = json.load(file)
    return data


def loadIntents():
    """ Loads the intents of the user (training of the bot) """
    with open(os.path.dirname(os.path.realpath(__file__)) + "\intents.json") as file:
        data = json.load(file)
    return data


def loadTrainedModel():
    """ Loads the trained model (the bot AI) """

    global dir_path

    return keras.models.load_model(dir_path)


def loadTokenizer():
    """ Load the tokenizer object """

    global dir_path

    with open(dir_path + '\\tokenizer.pickle', 'rb') as handle:
        tokenizer = pickle.load(handle)

    return tokenizer


def loadLabelEncoder():
    """ Load the label encoder object """

    global dir_path

    with open(dir_path + '\\label_encoder.pickle', 'rb') as enc:
        lbl_encoder = pickle.load(enc)

    return lbl_encoder


def loadBot():
    """ Loads the necessary binaries for the bot """
    try:        
        data = loadIntents()
        model = loadTrainedModel()
        tokenizer = loadTokenizer()
        lbl_encoder = loadLabelEncoder()
        entities = loadEntities()

        return data, model, tokenizer, lbl_encoder, entities
    except Exception as e: 
        logger.critical('Unable to load the bot [loadBot()]: ' + traceback.format_exc())
