import React, { useState, useEffect } from 'react'
import { View, Text, StyleSheet, FlatList, SafeAreaView, ScrollView, TouchableOpacity } from 'react-native'
import AsyncStorage from '@react-native-async-storage/async-storage'
import { url } from '../../utils/constants'

const Objetivos = () => {
    const [objetivos, setObjetivos] = useState([])

    useEffect(() => {
        BuscarObjetivos()
    }, [])

    const BuscarObjetivos = async () => {

        fetch(`${url}/objetivo`, {
            method : 'GET',
            headers : {
                'authorization' : `Bearer ${await AsyncStorage.getItem('@jwt')}`
            }
        })
        .then(response => response.json())
        .then(data => {
            setObjetivos(data)
            console.log(data)
        })
        .catch(error => console.log(error))
    }

    return (
        <SafeAreaView style={styles.container}>
            <Text style={styles.titlePage}>Objetivos</Text>
            <ScrollView>
                <FlatList
                    style={styles.listaObjetivos}
                    data={objetivos}
                    keyExtractor={objetivo => objetivo.id}
                    showsHorizontalScrollIndicator={false}
                    renderItem={({ item, index }) => (
                        <View style={styles.itemLista} key={index}>
                            <Text style={styles.titulo}>{item.titulo}</Text>
                            <Text style={styles.descricao}>{item.descricao}</Text>
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
    },
    titlePage : {
        color : '#8404D9',
        fontSize : 20,
        marginTop : 50,
        fontWeight : 'bold',
        textTransform : 'uppercase'
    },
    listaObjetivos : {
        marginTop : 25,
        marginHorizontal : 20,
    },
    itemLista : {
        width : 350,
        height : 180,
        padding : 10,
        textAlign : 'center',
        marginHorizontal : 30,
        marginTop : 30,
        borderRadius : 15,
        backgroundColor : '#c7c7c7',
    },
    titulo : {
        fontSize : 20,
        fontWeight : 500
    },
    descricao : {
        fontSize : 16,
        padding : 8
    }
  });

export default Objetivos