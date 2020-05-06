import { Effect, Reducer } from 'umi';
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
            yield put({
                type: 'fetchSuccess',
                payload: {
                    ...response,
                    form:{
                        ...response.form,
                        stime: moment(response.form.stime).format('YYYY-MM-DD HH:mm:ss'),
                    }
                },
            });
        },
        *approve({ payload }, { call }) {
            yield call(approveFinancial, payload);
            message.success('提交成功');
        },
        *reject({ payload }, { call }) {
            yield call(rejectFinancial, payload);
            message.success('提交成功');
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
