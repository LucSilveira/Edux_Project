import React, { useState } from 'react'
import { Image, KeyboardAvoidingView,View, Text, TextInput, StyleSheet,
            TouchableOpacity, Platform, TouchableWithoutFeedback, Keyboard } from 'react-native'
import AsyncStorage from '@react-native-async-storage/async-storage';
import { url } from '../../utils/constants'

const Login = ({ navigation }) => {
    const [email, setEmail] = useState('')
    const [senha, setSenha] = useState('')

    const salvar = async (token) => {
        await AsyncStorage.setItem('@jwt', token)
    }

    const Logar = () => {
        fetch(`${url}/login`, {
            method : 'POST',
            headers : {
                'content-type' : 'application/json'
            },
            body : JSON.stringify({
                'email' : email,
                'senha' : senha
            })
        }).then(response => response.json())
        .then(data => {
            if (data.status != 404) {
                alert('Seja bem vindo');

                salvar(data.token);
                navigation.push('Autenticado');
            } else {
                alert('Email ou senha inválidos! :( ');
            }
        })
    }

    return (
        <KeyboardAvoidingView enabled={Platform.OS === 'android'}
            behavior='padding'style={styles.container}>

            <TouchableWithoutFeedback onPress={Keyboard.dismiss}>
                <>
                    <Image source={require('../../assets/imgs/logo.png')} style={styles.imgLogo}/>
                    <Text style={styles.principalTitle}>Login</Text>

                    <View style={styles.forms}>
                        <TextInput style={styles.input} placeholder="Informe um email..." placeholderTextColor="#9200D6"
                            autoCapitalize="none" autoCorrect={false} value={email} onChangeText={txt => setEmail(txt)}/>

                        <TextInput style={styles.input} placeholder="Informe sua senha..." placeholderTextColor="#9200D6"
                            autoCapitalize="none" autoCorrect={false} secureTextEntry={true}  value={senha} onChangeText={txt => setSenha(txt)}/>

                        <TouchableOpacity style={styles.button} onPress={Logar}>
                            <Text style={styles.textButton}>Entrar</Text>
                        </TouchableOpacity>
                    </View>
                </>
            </TouchableWithoutFeedback>            
        </KeyboardAvoidingView>
    )
}

const styles = StyleSheet.create({
    container: {
      flex: 1,
      backgroundColor: '#9200D6',
      alignItems: 'center',
      justifyContent: 'center',
    },
    imgLogo : {
        width : 200,
        height : 100,
    },
    principalTitle : {
        fontSize : 24,
        textTransform : 'uppercase',
        color : '#FFF',
        fontWeight : 700
    },
    forms : {
        width : '65%',
        marginTop : 50
    },
    input : {
        width : '100%',
        height : 40,
        borderRadius : 8,
        padding : 8,
        backgroundColor : "#fff",
        marginVertical : 15,
        color : '#9200D6',
        fontSize : 16,
        fontWeight : 500
    },
    button : {
        width : '100%',
        height : 40,
        backgroundColor : '#fff',
        borderRadius : 8,
        alignItems : 'center',
        justifyContent : 'center',
        marginTop : 20
    },
    textButton : {
        color : '#9200D6',
        fontSize : 18,
        fontWeight : 700,
        textTransform : 'uppercase'
    }
});

export default Login;