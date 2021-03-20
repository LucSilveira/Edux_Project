import React, { useState, useEffect } from 'react'
import { View, Text, StyleSheet, FlatList, SafeAreaView, ScrollView, TouchableOpacity, Image, Platform } from 'react-native'
import { url } from '../../utils/constants'
import AsyncStorage from '@react-native-async-storage/async-storage'

const Alunos = ({ route, navigation }) => {
    const [alunos, setAlunos] = useState([])
    const { turmaId } = route.params

    useEffect(() => {
        BuscarAlunos()
    }, [])

    const BuscarAlunos = async () => {

        fetch(`${url}/alunoTurma/turma/${turmaId}`, {
            method : 'GET',
            headers : {
                'authorization' : `Bearer ${await AsyncStorage.getItem('@jwt')}`
            }
        })
        .then(response => response.json())
        .then(data => {
            setAlunos(data)
            console.log(data)
        })
        .catch(error => console.log(error))
    }

    return (
        <SafeAreaView style={styles.container}>
            <Text style={styles.titlePage}>Alunos</Text>
            <ScrollView>
                <FlatList
                    style={styles.listaAlunos}
                    data={alunos}
                    keyExtractor={aluno => aluno.id}
                    showsHorizontalScrollIndicator={false}
                    renderItem={({ item, index }) => (
                        <View style={styles.itemLista} key={index}>
                            <Image  style={styles.foto} source={ item.idUsuarioNavigation.imagem === 'padrao.jpg' ? require('../../assets/imgs/programmer.png')
                            : {uri : item.idUsuarioNavigation.imagem}}/>
                            <View style={styles.textoItem}>
                                <Text style={styles.titulo}>{item.idUsuarioNavigation.nome}</Text>
                                <Text style={styles.email}>{item.idUsuarioNavigation.email}</Text>
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
    listaAlunos : {
        marginTop : 25,
        marginHorizontal : 20,
        width : 300
    },
    itemLista : {
        width : '90%',
        height : 80,
        marginHorizontal : 30,
        marginTop : 30,
        borderRadius : 15,
        backgroundColor : '#c7c7c7',
        flexDirection : 'row',
        justifyContent : 'space-evenly',
        alignItems : 'center'
    },
    titulo : {
        fontSize : 20,
        fontWeight : 500
    },
    email : {
        fontSize : 18,
        fontWeight : 40,
        marginTop : 5
    },
    foto : {
        width : 65,
        height : 65,
        borderRadius : 100
    },
    
  });

export default Alunos