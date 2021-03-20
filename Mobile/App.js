import { StatusBar } from 'expo-status-bar';
import React from 'react';
import { StyleSheet, Text, View, TouchableOpacity } from 'react-native';
import { NavigationContainer } from '@react-navigation/native';
import { createBottomTabNavigator } from '@react-navigation/bottom-tabs';
import { createStackNavigator } from '@react-navigation/stack';
import { createDrawerNavigator } from '@react-navigation/drawer';

import Ionicons from 'react-native-vector-icons/Ionicons';
import MaterialCommunityIcons from 'react-native-vector-icons/MaterialCommunityIcons';
import AsyncStorage from '@react-native-async-storage/async-storage';

const Tab = createBottomTabNavigator();
const Stack = createStackNavigator();
const Drawer = createDrawerNavigator();

// Import das páginas
import Login from './pages/Login'
import DadosUsuario from './pages/Perfil'
import Turmas from './pages/Turmas'
import Alunos from './pages/Alunos'
import Objetivos from './pages/Objetivos'

const Menu = () => {
  <Drawer.Navigator>
      <Drawer.Screen name="Alunos" component={Alunos} />
      {/* <Drawer.Screen name="Posts" component={Posts} /> */}
    </Drawer.Navigator>
}

const Autenticado = () =>{
  return (
    <Tab.Navigator initialRouteName="Turmas"
    screenOptions={({ route }) => ({
      tabBarIcon: ({ focused, color, size }) => {
        let iconName;

        if (route.name === 'Perfil') {
          iconName = focused ? 'ios-person' : 'ios-person';
        } else if (route.name === 'Objetivos') {
          iconName = focused ? 'ios-checkbox' : 'ios-checkbox';
        } else if (route.name === 'Turmas') {
          iconName = focused ? 'ios-list-box' : 'ios-list';
        }

        return <Ionicons name={iconName} size={size} color={color} />;
      },
    })}
    tabBarOptions={{
      style: {
        backgroundColor: '#00D65F',
        borderStyle: 'solid'
      },
      activeTintColor: 'white',
      inactiveTintColor: '#333',
      fontFamily: 'Titillium Web'
    }}>
      <Tab.Screen name="Turmas" component={Turmas}/>
      <Tab.Screen name="Perfil" component={DadosUsuario}/>
      <Tab.Screen name="Objetivos" component={Objetivos}/>
    </Tab.Navigator>
  )
}

export default function App() {
  return (
    <NavigationContainer>
      <Stack.Navigator>
        <Stack.Screen name="Login" component={Login} options={{ headerShown: false }}/>
        <Stack.Screen name="Autenticado" component={Autenticado}options={{
          headerTitle: "Edux",
          headerTitleStyle: {
            fontSize: 36,
            fontWeight: "900",
            color: 'white',
            fontFamily: 'Titillium Web'
          },
          headerStyle: {
            backgroundColor: '#8404D9',
            borderBottomWidth: 0
          },
          headerLeft: null,
          headerRight: () => (
            <View>
              <TouchableOpacity
                onPress={() => {
                  AsyncStorage.removeItem('@jwt');
                  navigation.push('Login');
                }}
                style={{ marginRight: 20 }}
                underlayColor={"#8404D9"}
              >
                <MaterialCommunityIcons name="logout" color={"white"} size={30} />
              </TouchableOpacity>
            </View>
          )
        }} />
        <Stack.Screen name="Menu" component={Menu}/>
      </Stack.Navigator>
    </NavigationContainer>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#fff',
    alignItems: 'center',
    justifyContent: 'center',
  },
});
