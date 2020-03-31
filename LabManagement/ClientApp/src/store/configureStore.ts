import { applyMiddleware, combineReducers, compose, createStore } from 'redux';
import { connectRouter, routerMiddleware } from 'connected-react-router';
import { History } from 'history';
import { ApplicationState, reducers } from './';
import createSagaMiddleware from 'redux-saga';
import { all, fork } from 'redux-saga/effects';
export default function configureStore(history: History, initialState?: ApplicationState) {
    const sagaMiddleware = createSagaMiddleware();
    const middleware = [
        sagaMiddleware,
        routerMiddleware(history)
    ];

    const rootReducer = combineReducers({
        ...reducers,
        router: connectRouter(history)
    });

    const enhancers = [];
    const windowIfDefined = typeof window === 'undefined' ? null : window as any;
    if (windowIfDefined && windowIfDefined.__REDUX_DEVTOOLS_EXTENSION__) {
        enhancers.push(windowIfDefined.__REDUX_DEVTOOLS_EXTENSION__());
    }
    const store = createStore(
        rootReducer,
        initialState,
        compose(applyMiddleware(...middleware), ...enhancers)
    );
    sagaMiddleware.run(rootSaga);
    return store;
}

function* rootSaga(){
    yield all([

    ])
}