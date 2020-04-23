import {
  Card,
  Statistic,
  Descriptions,
  Divider,
  Tooltip,
  Empty,
  Table,
  Badge,
} from 'antd';
import { GridContent, PageHeaderWrapper } from '@ant-design/pro-layout';
import React, { FC, useEffect, useState } from 'react';
import { connect, Dispatch, FormToProcessState } from 'umi';
import styles from './style.less';
import DeclarationProcess from './component/DeclarationProcess';
import FinancialProcess from './component/FinancialProcess';
import ClaimProcess from './component/ClaimProcess';
interface FormToProcessProps {
  loading: boolean;
  myFormToProcess: FormToProcessState;
  dispatch: Dispatch
}

const FormToProcess: FC<FormToProcessProps> = (props) => {
  const { dispatch, myFormToProcess } = props;
  useEffect(() => {
    dispatch({
      type: 'myFormToProcess/fetchMessage',
    });
  }, []);
  const [tabKey, setTabKey] = useState<string>('declaration');
  const declearTodo = myFormToProcess.msg?.dform.filter(u => u.state === 'InProcess').length;
  const financialTodo = myFormToProcess.msg?.fform.filter(u => u.state === 'InProcess').length;
  const claimTodo = myFormToProcess.msg?.cform.filter(u => u.state === 'InProcess').length;
  const tabList = [
    {
      key: 'declaration',
      tab:
        <Badge count={declearTodo} offset={[10, 0]}><p>申报</p></Badge>,
    },
    {
      key: 'financial',
      tab: <Badge count={financialTodo} offset={[10, 0]}><p>财务申请</p></Badge>
    },
    {
      key: 'claimform',
      tab: <Badge count={claimTodo} offset={[10, 0]}><p>领用申请</p></Badge>
    },
  ];
  const contentList = {
    declaration: <DeclarationProcess />,
    claimform: <ClaimProcess />,
    financial: <FinancialProcess />
  };

  const TodoList = () => {
    if (!declearTodo || !financialTodo || !claimTodo)
      return null;
    if (declearTodo + financialTodo + claimTodo == 0) {
      return null;
    }
    return (
      <Card title='需要处理的申请' style={{ marginBottom: 24 }} bordered={false} tabList={tabList} onTabChange={setTabKey} activeTabKey={tabKey}>
        {contentList[tabKey]}
      </Card>
    )
  }
  return (
    <PageHeaderWrapper
      title="我的待办"
      className={styles.pageHeader}
    >
      <div className={styles.main}>
        <GridContent>
          <TodoList />
          <Card title='有状态更新的项目'style={{ marginBottom: 24 }} bordered={false}>
              2123
          </Card>
        </GridContent>
      </div>
    </PageHeaderWrapper>
  );
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
    myFormToProcess,
    loading: loading.effects['myFormToProcess/fetchAdvanced'],
  }),
)(FormToProcess);
