import React, { useEffect, useState } from 'react'
import { View, Text, StyleSheet, FlatList, SafeAreaView, ScrollView, TouchableOpacity } from 'react-native'
import { url } from '../../utils/constants'
import AsyncStorage from '@react-native-async-storage/async-storage'
import jwt_decode from 'jwt-decode'

const Turmas = ({ navigation }) => {
    const [turmas, setTurmas] = useState([])

    useEffect(() => {
        BuscarTurmas()
    }, [])

    const BuscarTurmas = async () => {
        const userId = jwt_decode(await AsyncStorage.getItem('@jwt')).IdUsuario

        fetch(`${url}/alunoTurma/aluno/${userId}`, {
            method : 'GET',
            headers : {
                'authorization' : `Bearer ${await AsyncStorage.getItem('@jwt')}`
            }
        })
        .then(response => response.json())
        .then(data => {
            setTurmas(data)
        })
        .catch(error => console.log(error))
    }

    return (
        <SafeAreaView style={styles.container}>
            <Text style={styles.titlePage}>Turmas</Text>
            <ScrollView>
                <FlatList
                    style={styles.listaTurmas}
                    data={turmas}
                    keyExtractor={turma => turma._id}
                    showsHorizontalScrollIndicator={false}
                    renderItem={({ item, index }) => (
                        <View style={styles.itemLista} key={index}>
                            <Text style={styles.titulo}>{item.idTurmaNavigation.idCursoNavigation.titulo}</Text>
                            <Text style={styles.descricao}>{item.idTurmaNavigation.descricao}</Text>
                            <View style={styles.areaButtons}>
                                <TouchableOpacity onPress={() => {
                                    navigation.navigate('Perfil', {
                                        alunoTurmaId : item.id,
                                        curso : item.idTurmaNavigation.idCursoNavigation.titulo
                                    })
                                }} style={[styles.botaoRanking, styles.button]}>
                                    <Text style={styles.textoButton}>Ver ranking</Text>
                                </TouchableOpacity>
                                <TouchableOpacity onPress={() => {
                                    navigation.navigate('Alunos', {
                                        turmaId : item.idTurmaNavigation.id
                                    })
                                }} style={[styles.botaoAlunos, styles.button]}>
                                    <Text style={styles.textoButton}>Ver alunos</Text>
                                </TouchableOpacity>
                            </View>
                        </View>
                    )}
                />
            </ScrollView>
        </SafeAreaView>
    )
}

const styles = StyleSheet.create({
    container: {
        flex: 1,
        height : 20,
        backgroundColor: '#fff',
        alignItems: 'center',
      //   justifyContent: 'center',
      },
    titlePage : {
        color : '#8404D9',
        fontSize : 20,
        marginTop : 50,
        fontWeight : 'bold',
        textTransform : 'uppercase'
    },
    listaTurmas : {
        marginTop : 25,
        marginHorizontal : 20
    },
    itemLista : {
        backgroundColor : '#c7c7c7',
        borderRadius : 15,
        textAlign : 'center',
    },
    areaButtons : {
        height  : 55,
        flexDirection : 'row',
        justifyContent : 'space-between',
    },
    button : {
        height : '100%',
        width : '50%',
        borderRadius : 15,
        justifyContent : 'center'
    },
    textoButton : {
        fontSize : 16,
        color : '#fff',
        fontWeight : 'bold'
    },
    botaoRanking : {
        backgroundColor : '#00C2EE',
        borderBottomRightRadius : 0,
        borderTopRightRadius : 0
    },
    botaoAlunos : {
        backgroundColor : '#F9E800',
        borderBottomLeftRadius : 0,
        borderTopLeftRadius : 0
    },
    titulo : {
        padding : 15,
        fontWeight : 'bold',
        fontSize : 20
    },
    descricao : {
        padding : 15,
        fontSize : 18,
        fontWeight : 400
    }
  });

export default Turmas