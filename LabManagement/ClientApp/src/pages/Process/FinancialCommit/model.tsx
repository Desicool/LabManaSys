import { Effect, Reducer, history } from 'umi';
import { queryFinancialDetail, approveFinancial, rejectFinancial } from './service';
import { IFinancialForm } from '@/models/entity';
import { message } from 'antd';
import moment from 'moment';

export interface FinancialProcessModelState {
    form?: IFinancialForm;
}

export interface FinancialProcessModelType {
    namespace: string;
    state: FinancialProcessModelState;
    effects: {
        fetch: Effect;
        approve: Effect;
        reject: Effect;
    };
    reducers: {
        fetchSuccess: Reducer<FinancialProcessModelState>;
    };
}

const Model: FinancialProcessModelType = {
    namespace: 'financialProcess',

    state: {
    },

    effects: {
        *fetch({ payload }, { call, put }) {
            const response = yield call(queryFinancialDetail, payload.formid);
            console.log(response);
            yield put({
                type: 'fetchSuccess',
                payload: {
                    ...response,
                    stime: moment(response.stime).format('YYYY-MM-DD HH:mm:ss'),
                },
            });
        },
        *approve({ payload }, { call }) {
            yield call(approveFinancial, payload);
            message.success('提交成功');
            history.push('/my/process');
        },
        *reject({ payload }, { call }) {
            yield call(rejectFinancial, payload);
            message.success('提交成功');
            history.push('/my/process');
        }
    },

    reducers: {
        fetchSuccess(state, action) {
            return {
                ...state,
                form: action.payload,
            };
        },
    },
};

export default Model;
