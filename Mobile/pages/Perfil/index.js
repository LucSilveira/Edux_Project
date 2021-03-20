import React, { useEffect, useState } from 'react'
import { View, Text, StyleSheet, Image, Platform } from 'react-native'
import Ionicons from 'react-native-vector-icons/Ionicons';
import MaterialCommunityIcons from 'react-native-vector-icons/MaterialCommunityIcons';
import AsyncStorage from '@react-native-async-storage/async-storage';
import { url } from '../../utils/constants'
import jwt_decode from 'jwt-decode'
import { set } from 'react-native-reanimated';

const DadosUsuario = ({ route }) =>{
    const [nome, setNome] = useState('');
    const [imagem, setImagem] = useState('')
    const [curso, setCurso] = useState('não identificamos um curso')
    const [alunoTurmaId, setAlunoTurmaId] = useState(0)
    
    useEffect(() => {
        BuscarInformacoes();
    }, [])

    useEffect(() => {
        route.params === undefined ? setAlunoTurmaId(0) : setAlunoTurmaId(route.params.alunoTurmaId)
        ListarObjetivos()
    }, route.params)

    const ListarObjetivos = async () => {
        // await route.params === undefined ? setAlunoTurmaId(0) : setAlunoTurmaId(route.params.alunoTurmaId)

        // console.log(alunoTurmaId)
    }

    const BuscarInformacoes = async () => {
        const userId = jwt_decode(await AsyncStorage.getItem('@jwt')).IdUsuario

        fetch(`${url}/usuario/${userId}`, {
            method : 'GET'
        })
        .then(response => response.json())
        .then(data => {
            setNome(data.nome)
            setImagem(data.imagem)
        })
        .catch(error => alert(error))
    }

    return (
        <>
            <View style={styles.container}>
                <Text style={styles.titlePage}>Ranking geral</Text>
                <View style={styles.infoUser}>
                    <Image  style={styles.foto} source={ imagem === 'padrao.jpg' ? require('../../assets/imgs/programmer.png')
                            : {uri : imagem}}/>   
                    <View style={styles.dados}>
                        <Text style={styles.dadosNome}>{nome}</Text>
                        <Text style={styles.dadosCurso}>{route.params === undefined ? curso : route.params.curso}</Text>
                    </View>
                </View>
                {
                    route.params === undefined
                    ? (
                        <View style={styles.alertRanking}>
                            <Text style={styles.textAlertRanking}>Selecione uma turma para ver sua colocação</Text>
                            <Ionicons name='ios-clipboard' color='#c7c7c7' size={55} />
                        </View>)
                    : (
                        <View style={styles.ranking}>
                            <View style={[styles.itemRanking, styles.allObjetivos]}>
                                <Text style={styles.posicao}>1º</Text>
                                <Text style={styles.quantidade}>40</Text>
                                <Text style={styles.descricao}>Objetivos concluidos</Text>
                            </View>
                            <View style={styles.itensHorizontal}>
                                <View style={[styles.itemRanking, styles.horizontalItem, styles.allPosts]}>
                                    <Text style={styles.posicao}>10º</Text>
                                    <Text style={styles.quantidade}>18</Text>
                                    <Text style={styles.descricao}>Posts curtidos</Text>
                                </View>
                                <View style={[styles.itemRanking, styles.horizontalItem, styles.allSecrects]}>
                                    <Text style={styles.posicao}>10º</Text>
                                    <Text style={styles.quantidade}>9</Text>
                                    <Text style={styles.descricao}>Segredos encontrados</Text>
                                </View>
                            </View>
                            <View style={[styles.itemRanking, styles.allNotas]}>
                                <Text style={styles.posicao}>8º</Text>
                                <Text style={styles.quantidade}>12</Text>
                                <Text style={styles.descricao}>Notas máximas</Text>
                            </View>
                        </View>
                    )
                }
            </View>
        </>
    )
}

const styles = StyleSheet.create({
    container: {
      flex: 1,
      height : 20,
      backgroundColor: '#fff',
      alignItems: 'center',
    },
    alertRanking : {
        width : 300,
        marginTop : 200,
        textAlign : 'center'
    },
    textAlertRanking : {
        fontSize : 24,
        marginBottom : 15,
        color : '#bfbfbf'
    },
    titlePage : {
        color : '#8404D9',
        fontSize : 20,
        marginTop : 50,
        fontWeight : 'bold',
        textTransform : 'uppercase'
    },
    infoUser : {
        backgroundColor : '#8404D9',
        width : '90%',
        height : 70,
        padding : 3,
        flexDirection : 'row',
        alignItems : 'center',
        borderRadius : 50,
        marginTop : 30
    },
    foto : {
        width : 65,
        height : 65,
        borderRadius : 100
    },
    dados : {
        marginLeft : 15
    },
    dadosNome : {
        color : '#fff',
        fontWeight : 'bold',
        fontSize : 18
    },
    dadosCurso : {
        color : '#fff',
        fontSize : 16
    },
    ranking : {
        marginTop : 80,
        alignItems : 'center'
    },
    itemRanking : {
        width : 145,
        height : 145,
        borderRadius : 100,
        textAlign : 'center',
        justifyContent : 'center',
        alignItems : 'center'
    },
    itensHorizontal : {
        flexDirection : 'row',
    },
    horizontalItem : {
        marginHorizontal : 25,
        marginVertical : -20
    },
    allObjetivos : {
        backgroundColor : '#00D65F'
    },
    allNotas : {
        backgroundColor : '#FF271C'
    },
    allPosts : {
        backgroundColor : '#00C2EE',
    },
    allSecrects : {
        backgroundColor : '#F9E800'
    },
    posicao : {
        color : '#fff',
        fontWeight : 'bold',
        fontSize : 24
    },
    quantidade : {
        color : '#fff',
        fontSize : 16,
        fontWeight : 600
    },
    descricao : {
        color : '#fff',
        fontWeight : 400,
        fontSize : 18
    }
});

export default DadosUsuario;