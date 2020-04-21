import React, { FC } from 'react';
import { connect, FormToProcessState, history } from 'umi';
import { IFinancialForm } from '@/models/entity';
import { Table } from 'antd';
import { ColumnsType } from 'antd/lib/table';

interface FinancialProcessProps {
    financials: IFinancialForm[];
    loading: boolean;
}
const FinancialProcessComponent: FC<FinancialProcessProps> = (props) => {
    const { financials } = props;
    const dataSource = financials.map(u => ({ ...u, key: u.id }));

    const columns: ColumnsType<IFinancialForm> = [
        {
            title: '编号',
            dataIndex: 'id',
            width: '6em'
        },
        {
            title:'申请人',
            dataIndex: 'uname',
            width: '12em'
        },
        {
            title: '申请金额',
            dataIndex: 'price'
        },
         {
            title: '进行处理',
            key: 'operation',
            render: (_, record) => <a onClick={()=>history.push('/process/financial/' + record.id)}>查看详情</a>,
        },
    ];
    return <div><Table dataSource={dataSource} columns={columns} />
    </div>
}

export default connect(
    ({
        myFormToProcess,
        loading,
    }: {
        myFormToProcess: FormToProcessState;
        loading: {
            effects: { [key: string]: boolean };
        };
    }) => ({
        financials: myFormToProcess.msg?.fform || [],
        loading: loading.effects['myFormToProcess/fetchAdvanced'],
    }),
)(FinancialProcessComponent);
